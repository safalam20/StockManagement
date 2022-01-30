using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StockManagement.Model;
using StockManagement.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Data
{
    public class EFSaleRepository : ISaleRepository
    {
        private SaleContext context;
        public EFSaleRepository(SaleContext ctx)
        {
            context = ctx;
        }
        
        public async Task<List<BestellungPositionPrice>> GetBestellungWithPrices()
        {
            string query = @"SELECT BB.Matchcode AS Matchcode,
  BB.A0Empfaenger AS Lieferant,
  BB.Belegnummer AS Belegnummer,
  Format(BB.Belegdatum, 'dd.MM.yyyy') AS BestellDatum,
  BB.VorID AS VorID,
  WP.Artikelnummer AS Artikelnummer,
  IIF(BB.Belegkennzeichen = 'EBC', WP.Menge, 0) AS TotalOrder,
  WP.Einzelpreis


FROM KHKEKBelege AS BB with(nolock)
 
  INNER JOIN KHKEKBelegePositionen AS WP with(nolock) ON WP.Mandant = BB.Mandant AND
    WP.BelID = BB.BelID


WHERE (BB.Matchcode LIKE 'CBW%' OR BB.Matchcode LIKE 'CBS%') AND BB.Mandant = 4
  AND BB.Belegkennzeichen = 'EBC'";

            var result = await context._EinkaufReportBelegPosition
              .FromSqlRaw(query)
              .AsNoTracking()
              .ToListAsync();

            return result;
        }
        public async Task<List<ArtikelData>> GetReifenArtikelDataString()
        {
            string sqlQuery = @"   SELECT 
       
KA.Artikelnummer, 
KA.USER_Produktionsgruppe,
KA.USER_ContainerStueck, 
KA.USER_ArtikelName,
KA.USER_Radius,
KA.USER_NB,
KA.USER_Breite,
KA.USER_Application,
KA.USER_txLZLK,
KA.USER_ArtikelTyp,
KA.Matchcode,
KA.Artikelgruppe,
KA.USER_LZ,
KA.USER_LK1,
KA.USER_LK2,
KA.USER_ET,
KA.USER_Farbekurz,
KA.USER_Farbelang,
KA.USER_EPRENr,

AV.Gewicht,

LB.Bestand,
ALF.MaxEK,
ALF.Lieferant,
WE.ConWar0,
WE.ConWarBetrag0,
WE.ConWarTS0,
WE.ConWarTSBetrag0,
WE.ConWar1,
WE.ConWarBetrag1,
WE.ConWarTS1,
WE.ConWarTSBetrag1,	
WE.ConWar2,
WE.ConWarBetrag2,
WE.ConWarTS2,
WE.ConWarTSBetrag2,

MB.Betrag0,
MB.Betrag1,
MB.Betrag2,
MB.Month1_0,
MB.Month2_0,
MB.Month3_0,
MB.Month4_0,
MB.Month5_0,
MB.Month6_0,
MB.Month7_0,
MB.Month8_0,
MB.Month9_0,
MB.Month10_0,
MB.Month11_0,
MB.Month12_0,
MB.Month1_1,
MB.Month2_1,
MB.Month3_1,
MB.Month4_1,
MB.Month5_1,
MB.Month6_1,
MB.Month7_1,
MB.Month8_1,
MB.Month9_1,
MB.Month10_1,
MB.Month11_1,
MB.Month12_1,
MB.Month1_2,
MB.Month2_2,
MB.Month3_2,
MB.Month4_2,
MB.Month5_2,
MB.Month6_2,
MB.Month7_2,
MB.Month8_2,
MB.Month9_2,
MB.Month10_2,
MB.Month11_2,
MB.Month12_2,
TP.T24Price,
KPA.Einzelpreis as ContainerDE

from KHKArtikel as KA WITH(NOLOCK)

--get Artikel weight

LEFT JOIN KHKArtikelVarianten as AV WITH(NOLOCK) on AV.Artikelnummer=KA.Artikelnummer and AV.Mandant=4


--get Tire24Price
LEFT JOIN(
select Q1.Artikelnummer, (ISNULL(Q1.Einzelpreis,0)+ISNULL(Q2.Shipping_Cost_Pcs,0)) as T24Price
from (select Artikelnummer,Einzelpreis from KHKPreislistenArtikel WITH(NOLOCK) where Mandant=6 and ListeID=64) as Q1

left outer join

(select Manufacturer_Number,Shipping_Cost_Pcs from [OLGlobal].[dbo].[KTShippingCosts] WITH(NOLOCK) where Shipping_Zone='DE_01n' ) as Q2 on Q1.Artikelnummer=Q2.Manufacturer_Number
) as TP on TP.Artikelnummer=KA.Artikelnummer


--get Container DE Price
LEFT JOIN  KHKPreislistenArtikel as KPA with(nolock) on KPA.Artikelnummer=KA.Artikelnummer and KPA.ListeID=108

--get stock with pgruppe
LEFT JOIN(
select IKA.USER_Produktionsgruppe, sum(HGS.Lagerbestand) as Bestand from HTKGlobalStock as HGS WITH(NOLOCK)
left join KHKArtikel as IKA WITH(NOLOCK) on IKA.Mandant=4 and IKA.Artikelnummer=HGS.Artikelnummer
where HGS.Mandant=0 and IKA.USER_SaleGroup!=6
group by IKA.USER_Produktionsgruppe
) as LB on KA.USER_Produktionsgruppe=LB.USER_Produktionsgruppe

--get price of active articles
OUTER APPLY
 (
 select top 1 L.Artikelnummer, L.Mandant,L.Einzelpreis as MaxEK,L.Lieferant from
 KHKArtikelLieferant AS L WITH(NOLOCK) where KA.Artikelnummer=L.Artikelnummer and L.Mandant=4
 order by Einzelpreis desc
 ) as ALF 

--get Container Wareingang
left join(
select 

IKA.USER_Produktionsgruppe,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.Menge else 0 end) as ConWar0,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.Menge else 0 end) as ConWar1,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.Menge else 0 end) as ConWar2,

SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.Menge else 0 end) as ConWarTS0,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.Menge else 0 end) as ConWarTS1,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.Menge else 0 end) as ConWarTS2,

SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.PBetrag else 0 end) as ConWarBetrag0,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.PBetrag else 0 end) as ConWarBetrag1,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.PBetrag else 0 end) as ConWarBetrag2,

SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.PBetrag else 0 end) as ConWarTSBetrag0,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.PBetrag else 0 end) as ConWarTSBetrag1,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.PBetrag else 0 end) as ConWarTSBetrag2

from fnKT_APPD_G02_R03_M_EinkaufReportBelegPosition() as E
left join  KHKArtikel as IKA WITH(NOLOCK) on IKA.Mandant=4 and IKA.Artikelnummer=E.Artikelnummer 
where E.Artikelgruppe='REIFEN' and (E.Belegart='Cont-TS-Wareneingang' or E.Belegart='Container-Wareneingang') and (E.Belegjahr=YEAR(GETDATE()) or E.Belegjahr=YEAR(GETDATE())-1  or E.Belegjahr=YEAR(GETDATE())-2)
group by IKA.USER_Produktionsgruppe
)as WE on KA.USER_Produktionsgruppe=WE.USER_Produktionsgruppe



left join(
select A.USER_Produktionsgruppe, 
--SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0)) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as SumMenge0,
--SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1)) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as SumMenge1,
--SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2)) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as SumMenge2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_2




 From KHKVKBelege AS B WITH(NOLOCK)

	LEFT JOIN KHKVKBelegePositionen AS P WITH(NOLOCK) ON P.Mandant = B.Mandant AND P.BelID = B.BelID
	LEFT JOIN KHKVKBelegarten as T WITH(NOLOCK) ON T.Kennzeichen = B.Belegkennzeichen
	LEFT JOIN KHKArtikel as A WITH(NOLOCK) ON P.Artikelnummer=A.Artikelnummer and A.Mandant=4

	Where B.Mandant = 6 and T.LagerbuchungWirkung != 0 and A.USER_Artikelgruppe='REIFEN' and 
	(B.Belegjahr=YEAR(GETDATE()) or B.Belegjahr=YEAR(GETDATE())-1  or B.Belegjahr=YEAR(GETDATE())-2) and
	A.USER_SaleGroup!=6

	group by A.USER_Produktionsgruppe
	) as MB on KA.USER_Produktionsgruppe=MB.USER_Produktionsgruppe



where KA.USER_Artikelgruppe='REIFEN' and KA.Aktiv=-1 and KA.USER_SaleGroup=1 and KA.Mandant=4;";

            var query = await context.ArtikelDatas
                 .FromSqlRaw(sqlQuery)
                 .AsNoTracking()
                 .ToListAsync();

            return query;
        }
        public async Task<List<StefanBackOrder>> GetFelgenBackOrdersWithDetails()
        {
            //            string sqlQuery = @"select 
            //STE.Matchcode,
            //STe.Lieferant,
            //STE.Jahr,
            //STE.Belegnummer,
            //STE.BestellDatum,
            //STE.VorID,
            //STE.BestellungReferenz,
            //STE.Artikelnummer,
            //STE.TotalOrder,
            //STE.TotalDelivered,
            //STE.BackOrder,
            //KA.USER_Produktionsgruppe

            //from fnKT_APPD_G02_R02_M_Stefan() as STE

            //left join KHKArtikel as KA with(nolock) on KA.Mandant=4 and KA.Artikelnummer=STE.Artikelnummer";

            string sqlQuery = @"SELECT BB.Matchcode AS Matchcode,
  BB.A0Empfaenger AS Lieferant,
  BB.Belegjahr AS Jahr,
  BB.Belegnummer AS Belegnummer,
  Format(BB.Belegdatum, 'dd.MM.yyyy') AS BestellDatum,
  BB.VorID AS VorID,
  BB.Referenzzeichen AS BestellungReferenz,
  WP.Artikelnummer AS Artikelnummer,
  KA.USER_Produktionsgruppe,
  Sum(IIF(WB.Belegkennzeichen = 'EBC', WP.Menge, 0)) AS TotalOrder,
  Sum(IIF(WB.Belegkennzeichen = 'ELC', WP.Menge, 0)) AS TotalDelivered,
 (Sum(IIF(WB.Belegkennzeichen = 'EBC', WP.Menge, 0)) - Sum(IIF(WB.Belegkennzeichen = 'ELC', WP.Menge, 0))) AS BackOrder,
 MAX(IIF(WB.Belegkennzeichen = 'EBC', WP.Einzelpreis , 0)) AS MaxPriceInBestellung

FROM KHKEKBelege AS BB
  INNER JOIN KHKEKBelege AS WB ON WB.Mandant = BB.Mandant AND
    WB.VorID = BB.VorID
  INNER JOIN KHKEKBelegePositionen AS WP ON WP.Mandant = BB.Mandant AND
    WP.BelID = WB.BelID

left join KHKArtikel as KA with(nolock) on KA.Mandant=4 and KA.Artikelnummer=WP.Artikelnummer

WHERE (BB.Matchcode LIKE 'CBW%' OR BB.Matchcode LIKE 'CBS%') AND BB.Mandant = 4
  AND BB.Belegkennzeichen = 'EBC'
GROUP BY BB.Matchcode,
  BB.A0Empfaenger,
  BB.Belegjahr,
  BB.Belegnummer,
  BB.VorID,
  BB.Referenzzeichen,
  WP.Artikelnummer,
  BB.Belegdatum,
  KA.USER_Produktionsgruppe
";


            var query = await context._BackOrders
                .FromSqlRaw(sqlQuery)
                .AsNoTracking()
                .ToListAsync();
            return query;
        }
        public async Task<List<StefanBackOrder>> GetReifenBackOrdersWithDetails()
        {

            string sqlQuery = @"SELECT BB.Matchcode AS Matchcode,
  BB.A0Empfaenger AS Lieferant,
  BB.Belegjahr AS Jahr,
  BB.Belegnummer AS Belegnummer,
  Format(BB.Belegdatum, 'dd.MM.yyyy') AS BestellDatum,
  BB.VorID AS VorID,
  BB.Referenzzeichen AS BestellungReferenz,
  WP.Artikelnummer AS Artikelnummer,
  KA.USER_Produktionsgruppe,
  Sum(IIF(WB.Belegkennzeichen = 'EBC', WP.Menge, 0)) AS TotalOrder,
  Sum(IIF(WB.Belegkennzeichen = 'ELC', WP.Menge, 0)) AS TotalDelivered,
 (Sum(IIF(WB.Belegkennzeichen = 'EBC', WP.Menge, 0)) - Sum(IIF(WB.Belegkennzeichen = 'ELC', WP.Menge, 0))) AS BackOrder,
 MAX(IIF(WB.Belegkennzeichen = 'EBC', WP.Einzelpreis , 0)) AS MaxPriceInBestellung

FROM KHKEKBelege AS BB
  INNER JOIN KHKEKBelege AS WB ON WB.Mandant = BB.Mandant AND
    WB.VorID = BB.VorID
  INNER JOIN KHKEKBelegePositionen AS WP ON WP.Mandant = BB.Mandant AND
    WP.BelID = WB.BelID

left join KHKArtikel as KA with(nolock) on KA.Mandant=4 and KA.Artikelnummer=WP.Artikelnummer

WHERE (BB.Matchcode LIKE 'CBW%' OR BB.Matchcode LIKE 'CBS%') AND BB.Mandant = 4
  AND BB.Belegkennzeichen = 'EBC'
GROUP BY BB.Matchcode,
  BB.A0Empfaenger,
  BB.Belegjahr,
  BB.Belegnummer,
  BB.VorID,
  BB.Referenzzeichen,
  WP.Artikelnummer,
  BB.Belegdatum,
  KA.USER_Produktionsgruppe
";


            var query = await context._BackOrders
                .FromSqlRaw(sqlQuery)
                .AsNoTracking()
                .ToListAsync();

            return query;
        }       
        public async Task<List<ArtikelData>> GetFelgenArtikelDataWithString()
        {
            string sqlQuery = @"select
KA.Artikelnummer,
KA.USER_Produktionsgruppe,
KA.USER_ContainerStueck, 
KA.USER_ArtikelName,
KA.USER_Radius,
KA.USER_NB,
KA.USER_Breite,
KA.USER_Application,
KA.USER_txLZLK,
KA.USER_ArtikelTyp,
KA.Matchcode,
KA.Artikelgruppe,
KA.USER_LZ,
KA.USER_LK1,
KA.USER_LK2,
KA.USER_ET,
KA.USER_Farbekurz,
KA.USER_Farbelang,
KA.USER_EPRENr,

AV.Gewicht,
TP.T24Price,
KPA.Einzelpreis as ContainerDE,
LB.Bestand,
ALF.MaxEK,
ALF.Lieferant,
WE.ConWar0,
WE.ConWarBetrag0,
WE.ConWarTS0,
WE.ConWarTSBetrag0,
WE.ConWar1,
WE.ConWarBetrag1,
WE.ConWarTS1,
WE.ConWarTSBetrag1,	
WE.ConWar2,
WE.ConWarBetrag2,
WE.ConWarTS2,
WE.ConWarTSBetrag2,
MB.Betrag0,
MB.Betrag1,
MB.Betrag2,
MB.Month1_0,
MB.Month2_0,
MB.Month3_0,
MB.Month4_0,
MB.Month5_0,
MB.Month6_0,
MB.Month7_0,
MB.Month8_0,
MB.Month9_0,
MB.Month10_0,
MB.Month11_0,
MB.Month12_0,
MB.Month1_1,
MB.Month2_1,
MB.Month3_1,
MB.Month4_1,
MB.Month5_1,
MB.Month6_1,
MB.Month7_1,
MB.Month8_1,
MB.Month9_1,
MB.Month10_1,
MB.Month11_1,
MB.Month12_1,
MB.Month1_2,
MB.Month2_2,
MB.Month3_2,
MB.Month4_2,
MB.Month5_2,
MB.Month6_2,
MB.Month7_2,
MB.Month8_2,
MB.Month9_2,
MB.Month10_2,
MB.Month11_2,
MB.Month12_2
from KHKArtikel as KA with(nolock)


--get Artikel weight

LEFT JOIN KHKArtikelVarianten as AV WITH(NOLOCK) on AV.Artikelnummer=KA.Artikelnummer and AV.Mandant=4

--get Tire24Price
LEFT JOIN(
select Q1.Artikelnummer, (ISNULL(Q1.Einzelpreis,0)+ISNULL(Q2.Shipping_Cost_Pcs,0)) as T24Price
from (select Artikelnummer,Einzelpreis from KHKPreislistenArtikel WITH(NOLOCK) where Mandant=6 and ListeID=64) as Q1
left outer join
(select Manufacturer_Number,Shipping_Cost_Pcs from [OLGlobal].[dbo].[KTShippingCosts] WITH(NOLOCK) where Shipping_Zone='DE_01n' ) as Q2 on Q1.Artikelnummer=Q2.Manufacturer_Number
) as TP on TP.Artikelnummer=KA.Artikelnummer

--get Container DE Price
LEFT JOIN  KHKPreislistenArtikel as KPA with(nolock) on KPA.Artikelnummer=KA.Artikelnummer and KPA.ListeID=108

--get stock with pgruppe
LEFT JOIN(
select HGS.Artikelnummer, HGS.Lagerbestand as Bestand from HTKGlobalStock as HGS with(nolock)
where HGS.Mandant=0
) as LB on KA.Artikelnummer=LB.Artikelnummer


--get price of active articles
OUTER APPLY
 (
 select top 1 L.Artikelnummer, L.Mandant,L.Einzelpreis as MaxEK,L.Lieferant from
 KHKArtikelLieferant AS L WITH(NOLOCK) where KA.Artikelnummer=L.Artikelnummer and L.Mandant=4
 order by Einzelpreis desc
 ) as ALF 


 --get Container Wareingang
left join(
select 

E.Artikelnummer,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.Menge else 0 end) as ConWar0,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.Menge else 0 end) as ConWar1,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.Menge else 0 end) as ConWar2,

SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.Menge else 0 end) as ConWarTS0,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.Menge else 0 end) as ConWarTS1,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.Menge else 0 end) as ConWarTS2,

SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.PBetrag else 0 end) as ConWarBetrag0,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.PBetrag else 0 end) as ConWarBetrag1,
SUM(CASE WHEN (Belegart='Container-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.PBetrag else 0 end) as ConWarBetrag2,

SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())) then E.PBetrag else 0 end) as ConWarTSBetrag0,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-1) then E.PBetrag else 0 end) as ConWarTSBetrag1,
SUM(CASE WHEN (Belegart='Cont-TS-Wareneingang' and Belegjahr=YEAR(GETDATE())-2) then E.PBetrag else 0 end) as ConWarTSBetrag2

from fnKT_APPD_G02_R03_M_EinkaufReportBelegPosition() as E

where (E.Artikelgruppe='ALU' or E.Artikelgruppe='STAHL') and (E.Belegart='Cont-TS-Wareneingang' or E.Belegart='Container-Wareneingang') and 
(E.Belegjahr=YEAR(GETDATE()) or E.Belegjahr=YEAR(GETDATE())-1  or E.Belegjahr=YEAR(GETDATE())-2)
group by E.Artikelnummer
)as WE on KA.Artikelnummer=WE.Artikelnummer


--Get Mothly Sales
left join(
select P.Artikelnummer, 

SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2)) then P.Menge*P.Einzelpreis*T.LagerbuchungWirkung*-1 else 0 end) as Betrag2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-0) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_0,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-1) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_1,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=1) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month1_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=2) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month2_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=3) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month3_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=4) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month4_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=5) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month5_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=6) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month6_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=7) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month7_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=8) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month8_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=9) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month9_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=10) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month10_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=11) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month11_2,
SUM(CASE WHEN (YEAR(B.Belegdatum)=(YEAR(GETDATE())-2) and MONTH(B.Belegdatum)=12) then P.Menge*T.LagerbuchungWirkung*-1 else 0 end) as Month12_2




 From KHKVKBelege AS B WITH(NOLOCK)

	LEFT JOIN KHKVKBelegePositionen AS P WITH(NOLOCK) ON P.Mandant = B.Mandant AND P.BelID = B.BelID
	LEFT JOIN KHKVKBelegarten as T WITH(NOLOCK) ON T.Kennzeichen = B.Belegkennzeichen

	Where B.Mandant = 6 and T.LagerbuchungWirkung != 0 and (P.USER_Artikelgruppe='ALU' or P.USER_Artikelgruppe='STAHL') and 
	(B.Belegjahr=YEAR(GETDATE()) or B.Belegjahr=YEAR(GETDATE())-1  or B.Belegjahr=YEAR(GETDATE())-2)
	group by P.Artikelnummer
	) as MB on KA.Artikelnummer=MB.Artikelnummer


where (KA.USER_Artikelgruppe='ALU' or KA.USER_Artikelgruppe='STAHL') and KA.Aktiv=-1 and KA.USER_SaleGroup=1 and KA.Mandant=4;";



            var query = await context.ArtikelDatas
                .FromSqlRaw(sqlQuery)
                .AsNoTracking()
                .ToListAsync();

            return query;
        }      
        public async Task<List<WarenEingangData>> GetFelgenWareneingang()
        {
            string sqlQuery = @" select 
EBP.Referenznummer,
EBP.Belegkennzeichen,
EBP.Belegart,
EBP.Artikelgruppe,
EBP.Artikelnummer,
KA.USER_Produktionsgruppe,
EBP.Belegdatum,
EBP.A0Name1,
EBP.A0Empfaenger,
EBP.Menge,
EBP.Einzelpreis,
MCD.USER_ETA,
MCD.USER_ETD,
MCD.Lieferant,
MCD.USER_Rechnungsnummer,
MCD.USER_Referenznummer,
MCD.USER_Spedition,
MCD.BLNummer,
MCD.BelID as TransItems

from fnKT_APPD_G02_R03_M_EinkaufReportBelegPosition() as EBP


left join
(
select CD.BelID, CD.USER_ETA, CD.USER_ETD,CD.USER_Referenznummer,CD.USER_Rechnungsnummer,CD.Lieferant,CD.USER_Spedition,CD.BLNummer
from fnKT_APPD_G02_R04_M_ContainerDetail() as CD
where (CD.Belegart='Container-Wareneingang' or CD.Belegart='Cont-TS-Wareneingang') and CD.ContainerLagerErhaltenDatumundZeit is null 
and CONVERT(datetime,CD.Belegdatum)> CONVERT(datetime,'01-01-2021')
)as MCD on EBP.BelID=MCD.BelID

left join KHKArtikel as KA with(nolock) on EBP.Artikelnummer=KA.Artikelnummer and KA.Mandant=4

where (EBP.Belegart='Container-Wareneingang' or EBP.Belegart='Cont-TS-Wareneingang') and 
(EBP.Artikelgruppe='REIFEN' or EBP.Artikelgruppe='ALU' or EBP.Artikelgruppe='STAHL')";


            var query = await context._WarenEingangDatas
                 .FromSqlRaw(sqlQuery)
                 .AsNoTracking()
                 .Where(x => x.Artikelgruppe == "ALU" || x.Artikelgruppe == "STAHL")
                 .OrderByDescending(x => x.Belegdatum)
                 .ToListAsync();


            return query;
        }
        public async Task<List<WarenEingangData>> GetReifenWareneingang()
        {
            string query = @" select 
EBP.Referenznummer,
EBP.Belegkennzeichen,
EBP.Belegart,
EBP.Artikelgruppe,
EBP.Artikelnummer,
KA.USER_Produktionsgruppe,
EBP.Belegdatum,
EBP.A0Empfaenger,
EBP.A0Name1,
EBP.Menge,
EBP.Einzelpreis,
MCD.USER_ETA,
MCD.USER_ETD,
MCD.Lieferant,
MCD.USER_Rechnungsnummer,
MCD.USER_Referenznummer,
MCD.USER_Spedition,
MCD.BLNummer,
MCD.BelID as TransItems

from fnKT_APPD_G02_R03_M_EinkaufReportBelegPosition() as EBP


left join
(
select CD.BelID, CD.USER_ETA, CD.USER_ETD,CD.USER_Referenznummer,CD.USER_Rechnungsnummer,CD.Lieferant,CD.USER_Spedition,CD.BLNummer
from fnKT_APPD_G02_R04_M_ContainerDetail() as CD
where (CD.Belegart='Container-Wareneingang' or CD.Belegart='Cont-TS-Wareneingang') and CD.ContainerLagerErhaltenDatumundZeit is null 
and CONVERT(datetime,CD.Belegdatum)> CONVERT(datetime,'01-01-2021')
)as MCD on EBP.BelID=MCD.BelID

left join KHKArtikel as KA with(nolock) on EBP.Artikelnummer=KA.Artikelnummer and KA.Mandant=4

where (EBP.Belegart='Container-Wareneingang' or EBP.Belegart='Cont-TS-Wareneingang') and 
(EBP.Artikelgruppe='REIFEN' or EBP.Artikelgruppe='ALU' or EBP.Artikelgruppe='STAHL')";
            var result = await context._WarenEingangDatas
                 .FromSqlRaw(query)
                 .AsNoTracking()
                 .Where(x => x.Artikelgruppe == "REIFEN")
                 .OrderByDescending(x => x.Belegdatum)
                 .ToListAsync();


            return result;
        }
        public async Task<List<CostVariables>> GetCostsVariables()
        {
            var query = await context._VariablesCost               
                .ToListAsync();

            return query;
        }
        public async Task<List<LieferantZoll>> GetAllLieferantZoll()
        {
            var query = await context._LieferantZoll
                .ToListAsync();

            return query;
        }
        public async Task SaveChangesZoll(LieferantZoll lif)
        {
            context._LieferantZoll.Add(lif);
            await context.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {           
            await context.SaveChangesAsync();

        }
        public async Task<List<KTEnumerator>> GetUsers()
        {
            var ktUsers = await context.KTEnumerators
                .AsNoTracking()
                .Where(k => k.Type1.Equals("StockManagement"))
                .ToListAsync();

            return ktUsers;

        }
        public async Task<List<ProductionOffer>> GetTempProduktionOffersReifen()
        {
            var query = await context._ProductionOffer
                .Where(x=>x.Artikelgruppe=="REIFEN")
                .ToListAsync();

            return query;

        }
        public async Task<List<ProductionOffer>> GetTempProduktionOffersFelgen()
        {
            var query = await context._ProductionOffer
                .Where(x => x.Artikelgruppe == "FELGEN")
                .ToListAsync();

            return query;

        }
        public void AddToTempOrder(ProductionOffer toAdd)
        {
            context._ProductionOffer.Add(toAdd);
        }
        public async Task<List<ArtikelLieferantDTO>> GetArtikelLieferantsReifen()
        {
            string query = @"select 
KAL.Artikelnummer,
KA.USER_Produktionsgruppe,
KAL.Lieferant,
KAL.Einzelpreis
from KHKArtikelLieferant as KAL with(nolock)
left join KHKArtikel as KA with(nolock) on KA.Artikelnummer=KAL.Artikelnummer 
where KA.Aktiv=-1 and KA.USER_SaleGroup=1 and KA.Mandant=4 and KAL.Mandant=4 and KA.USER_Artikelgruppe='REIFEN';";

            var result = await context._ArtikelLieferants
                .FromSqlRaw(query)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        public async Task<List<ArtikelLieferantDTO>> GetArtikelLieferantsFelgen()
        {
            string query = @"select 
KAL.Artikelnummer,
KA.USER_Produktionsgruppe,
KAL.Lieferant,
KAL.Einzelpreis
from KHKArtikelLieferant as KAL with(nolock)
left join KHKArtikel as KA with(nolock) on KA.Artikelnummer=KAL.Artikelnummer 
where KA.Aktiv=-1 and KA.USER_SaleGroup=1 and KA.Mandant=4 and KAL.Mandant=4 and (KA.USER_Artikelgruppe='ALU' or KA.USER_Artikelgruppe='STAHL');";
            var result = await context._ArtikelLieferants
                .FromSqlRaw(query)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        public async Task<List<KHKOpUbersicht>> GetKhkOpUbersichtUnterwegs(string whereInQuery)
        {

            var result = await context._KHKOpUbersichts
               .FromSqlRaw($"select * from fnKT_APPD_G02_R05_M_OP_Ubersicht() where LF_Rechnungnummer in ({whereInQuery})")
               .AsNoTracking()
               .ToListAsync();

            return result;
        }
        public async Task<int> Test()
        {

            var count = await context.Database
                .ExecuteSqlRawAsync("UPDATE KTEnumerator SET Name1 = 'Produktion' WHERE ID = 28;");
            //.ExecuteSqlRawAsync(@"INSERT INTO KTEnumerator(Aktiv,Type1,Type2,Pro,Sort,Enumerator,Name1 ) VALUES(1, 'StockManagement', 'Benutzer', 1, 1, 'KT\Bassem.Aouini', 'Vertrieb')");
            return count;
        }


    }
}
