//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ProjetV3R_Employe.Data.Models.ProjetV3R;
//using ProjetV3R_Employe.Data.Models;

//namespace ProjetV3R.Services
//{
//    public class FournisseurService
//    {
//        private readonly ApplicationDbContext2 _dbContext;

//        public FournisseurService(ApplicationDbContext2 dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<List<Fournisseur>> ObtenirFournisseursAsync()
//        {
//            try
//            {
//                return await _dbContext.Fournisseurs.ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Erreur lors de la récupération des fournisseurs : {ex.Message}");
//                return new List<Fournisseur>();
//            }
//        }
//    }
//}
