using StockManagement.Model;
using StockManagement.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Data
{
    public interface ISaleRepository
    {

        Task SaveChangesZoll(LieferantZoll lif);
        Task SaveChanges();
        Task<List<CostVariables>> GetCostsVariables();
        Task<List<LieferantZoll>> GetAllLieferantZoll();
        Task<List<WarenEingangData>> GetFelgenWareneingang();
        Task<List<WarenEingangData>> GetReifenWareneingang();
        Task<List<KTEnumerator>> GetUsers();
        Task<List<ArtikelData>> GetFelgenArtikelDataWithString();
        Task<List<ArtikelData>> GetReifenArtikelDataString();
        Task<List<ProductionOffer>> GetTempProduktionOffersReifen();
        Task<List<ProductionOffer>> GetTempProduktionOffersFelgen();
        void AddToTempOrder(ProductionOffer toAdd);
        Task<List<StefanBackOrder>> GetReifenBackOrdersWithDetails();
        Task<List<StefanBackOrder>> GetFelgenBackOrdersWithDetails();
        Task<List<BestellungPositionPrice>> GetBestellungWithPrices();
        Task<List<ArtikelLieferantDTO>> GetArtikelLieferantsReifen();
        Task<List<ArtikelLieferantDTO>> GetArtikelLieferantsFelgen();
        Task<List<KHKOpUbersicht>> GetKhkOpUbersichtUnterwegs(string whereInQuery);
        Task<int> Test();

    }
        
}
