using Books;


using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BoutiqueDataBase
{
    private readonly SQLiteAsyncConnection _baseDeDonnees;
    public BoutiqueDataBase(string dbPath)
    {
        _baseDeDonnees = new SQLiteAsyncConnection(dbPath);
        _baseDeDonnees.CreateTableAsync<Categorie>().Wait();
        _baseDeDonnees.CreateTableAsync<Produit>().Wait();
       // _baseDeDonnees.DropTableAsync<LigneCommande>().Wait();
        _baseDeDonnees.CreateTableAsync<LigneCommande>().Wait();
       // _baseDeDonnees.DropTableAsync<Commande>().Wait();
        _baseDeDonnees.CreateTableAsync<Commande>().Wait();
    }

    // Opérations sur les catégories
    public Task<List<Categorie>> ObtenirCategories()
    {
        return _baseDeDonnees.Table<Categorie>().ToListAsync();
    }
    public Task<int> SaveCategorieAsync(Categorie categorie)
    {
        if (categorie.Id != 0)
        {
            return _baseDeDonnees.UpdateAsync(categorie);
        }
        else
        {
            return _baseDeDonnees.InsertAsync(categorie);
        }
    }
    public  void SupprimerCategorie(int idCategorie)
    {
        _baseDeDonnees.DeleteAsync<Categorie>(idCategorie);
    }
    
    // Opérations sur les produits
    
    public Task<List<Produit>> ObtenirProduits(int idCategorie)
    {
        return _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie ==idCategorie).ToListAsync();
    }
    public Task<List<Produit>> ObtenirToutProduits()
    {
        return _baseDeDonnees.Table<Produit>().ToListAsync();
    }
    public Task<int> AjouterProduit(Produit produit)
    {
        if (produit.Id != 0)
        {
            return _baseDeDonnees.UpdateAsync(produit);
        }
        else
        {
            return _baseDeDonnees.InsertAsync(produit);
        }

    }
    
    public void ModifierProduit(Produit produit)
    {
        _baseDeDonnees.UpdateAsync(produit);
    }
    public void SupprimerProduit(int idProduit)
    {
        _baseDeDonnees.DeleteAsync<Produit>(idProduit);
    }





    // Opérations sur les lignes de commande
    public async  Task<int> AjouterLigneCommande(LigneCommande ligneCommande)
    {
       return await _baseDeDonnees.InsertAsync(ligneCommande);
    }

    // Opérations sur les commandes
    public async Task<int>AjouterCommande(Commande commande)
    {
        return await _baseDeDonnees.InsertAsync(commande);
    }

    /*public async Task<List<LigneCommande>> ObtenirLignesCommande(int idCommande)
    {
        return await _baseDeDonnees.Table<LigneCommande>().Where(l => l.IdCommande == idCommande).ToListAsync();
    }*/
    public Task<List<Commande>> ObtenirToutCommande()
    {
        return _baseDeDonnees.Table<Commande>().ToListAsync();
    }

   
    //
    public async Task<int> AjouterLigneCommandewithId(LigneCommande ligneCommande)
    {
        await _baseDeDonnees.InsertAsync(ligneCommande);
        return ligneCommande.Id;
    }
    //
    public async Task<List<LigneCommande>> GetLigneCommandesByIds(List<int> ids)
    {
        var ligneCommandes = await _baseDeDonnees.Table<LigneCommande>().Where(l => ids.Contains(l.Id)).ToListAsync();
        return ligneCommandes;
    }
    // ... (ajoutez d'autres opérations selon vos besoins)
}

