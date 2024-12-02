using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjetV3R_Employe.Data.Models;
using ProjetV3R_Employe.Data.Models.ProjetV3R;
using System.Security.Cryptography;
using System.Text;

public class FournisseurService
{
    private readonly ApplicationDbContext2 _dbContext;

    public Action? OnFournisseursChanged { get; set; }

    public FournisseurService(ApplicationDbContext2 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Fournisseur>> ObtenirFournisseursAsync()
    {
        try
        {
            return await _dbContext.Fournisseurs.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des fournisseurs : {ex.Message}");
            return new List<Fournisseur>();
        }
    }

    public async Task<Fournisseur?> ObtenirFournisseurParIdAsync(int id)
    {
        try
        {
            return await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération du fournisseur : {ex.Message}");
            return null;
        }
    }
    public async Task MettreAJourEtatCompteAsync(int fournisseurId, bool nouvelEtat)
    {
        try
        {
            var fournisseur = await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == fournisseurId);
            if (fournisseur != null)
            {
                fournisseur.EtatCompte = nouvelEtat;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Fournisseur introuvable.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la mise à jour de l'état du compte : {ex.Message}");
            throw;
        }
    }

    public async Task UpdateEtatDemandeAsync(int fournisseurId, string nouvelEtat)
    {
        try
        {
            var fournisseur = await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == fournisseurId);

            if (fournisseur != null)
            {
                fournisseur.EtatDemande = nouvelEtat;
                if(nouvelEtat == "Approuvée")
                {
                    fournisseur.EtatCompte = true;
                }
                else
                {
                    fournisseur.EtatCompte = false;
                }
                    await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Fournisseur introuvable dans la base de données.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la mise à jour de l'état de la demande : {ex.Message}");
            throw;
        }
    }

    // Add a new Fournisseur
    //public async Task AjouterFournisseurAsync(Fournisseur fournisseur)
    //{
    //    try
    //    {
    //        if (string.IsNullOrWhiteSpace(fournisseur.NomEntreprise))
    //        {
    //            throw new ArgumentException("Le nom de l'entreprise est obligatoire.");
    //        }

    //        if (string.IsNullOrWhiteSpace(fournisseur.Neq))
    //        {
    //            throw new ArgumentException("Le NEQ est obligatoire.");
    //        }

    //        _dbContext.Fournisseurs.Add(fournisseur);
    //        await _dbContext.SaveChangesAsync();
    //        OnFournisseursChanged?.Invoke();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception($"Erreur lors de l'ajout du fournisseur : {ex.Message}");
    //    }
    //}

    //// Update an existing Fournisseur
    //public async Task ModifierFournisseurAsync(Fournisseur fournisseur)
    //{
    //    try
    //    {
    //        var fournisseurExistant = await _dbContext.Fournisseurs.FirstOrDefaultAsync(f => f.FournisseurId == fournisseur.FournisseurId);
    //        if (fournisseurExistant == null)
    //        {
    //            throw new Exception("Le fournisseur à modifier n'existe pas.");
    //        }

    //        fournisseurExistant.NomEntreprise = fournisseur.NomEntreprise;
    //        fournisseurExistant.Neq = fournisseur.Neq;
    //        fournisseurExistant.EtatDemande = fournisseur.EtatDemande;
    //        fournisseurExistant.EtatCompte = fournisseur.EtatCompte;
    //        fournisseurExistant.CourrielEntreprise = fournisseur.CourrielEntreprise;
    //        fournisseurExistant.DetailsEntreprise = fournisseur.DetailsEntreprise;
    //        fournisseurExistant.SiteWeb = fournisseur.SiteWeb;

    //        await _dbContext.SaveChangesAsync();
    //        OnFournisseursChanged?.Invoke();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception($"Erreur lors de la modification du fournisseur : {ex.Message}");
    //    }
    //}

    //// Delete a Fournisseur
    //public async Task SupprimerFournisseurAsync(int fournisseurId)
    //{
    //    try
    //    {
    //        var fournisseur = await _dbContext.Fournisseurs.FindAsync(fournisseurId);
    //        if (fournisseur == null)
    //        {
    //            throw new Exception("Le fournisseur n'existe pas.");
    //        }

    //        _dbContext.Fournisseurs.Remove(fournisseur);
    //        await _dbContext.SaveChangesAsync();
    //        OnFournisseursChanged?.Invoke();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception($"Erreur lors de la suppression du fournisseur : {ex.Message}");
    //    }
    //}

    public async Task<ICollection<Adress>?> ObtenirAdressesFournisseurParIdAsync(int fournisseurId)
    {
        try
        {
            var fournisseur = await _dbContext.Fournisseurs
                .Where(f => f.FournisseurId == fournisseurId)
                .Include(f => f.Adresses) // Ensure that the Adresses collection is included
                .FirstOrDefaultAsync();

            if (fournisseur == null)
            {
                throw new Exception("Fournisseur introuvable.");
            }

            return fournisseur.Adresses; // Return the collection of addresses
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des adresses : {ex.Message}");
            return null;
        }
    }


    public bool _isCreationForm = true;

    private static readonly object _lock = new object();

    // Properties to hold form data for identification
    public string NomEntrepriseInput { get; set; } = string.Empty;
    public string NeqInput { get; set; } = string.Empty;
    public string EmailInput { get; set; } = string.Empty;
    public string PasswordInput { get; set; } = string.Empty;
    public string RepeatPasswordInput { get; set; } = string.Empty;

    // Properties to hold form data for address
    public string NumCiviqueInput { get; set; } = string.Empty;
    public string BureauInput { get; set; } = string.Empty;
    public string RueInput { get; set; } = string.Empty;
    public string MunicipaliteInput { get; set; } = string.Empty;
    public string ProvinceInput { get; set; } = string.Empty;
    public string CodePostalInput { get; set; } = string.Empty;
    public string RegionInput { get; set; } = string.Empty;
    public string NumeroTelephoneInput { get; set; } = string.Empty;
    public string NumeroPosteInput { get; set; } = string.Empty;
    public string SiteWebInput { get; set; } = string.Empty;
    public string NomMunicipaliteInput { get; set; } = string.Empty;

    // Properties to hold data from contacts
    public List<ContactInput> ContactsInput = new List<ContactInput>();

    // Properties to hold data from produits/services 
    public string DescriptionProduitsServicesInput { get; set; } = string.Empty;
    public List<UnspscComodite> ProduitsServicesSelectionnesInput = new List<UnspscComodite>();

    // Properties to hold data from RBQ
    public string RBQNumberInput { get; set; } = string.Empty;
    public string SelectedStatus { get; set; } = string.Empty;
    public string SelectedLicenseType { get; set; } = string.Empty;
    public string SelectedCategory { get; set; } = string.Empty;
    public DateTime StartDateInput { get; set; } = DateTime.Now;
    public DateTime EndDateInput { get; set; } = DateTime.Now;

    public List<Souscategoriesafter2008> SelectedSubCategories = new List<Souscategoriesafter2008>();

    // Section pour Brochures 
    public string UploadDirectory { get; set; } = string.Empty;
    public string OriginalBrochureName { get; set; } = string.Empty;
    public string OriginalCarteAffaireName { get; set; } = string.Empty;
    public Brochure SelectedBrochure = new Brochure();
    public Brochure SelectedCarteAffaire = new Brochure();
    public IBrowserFile BrochureFile { get; set; } = null;
    public IBrowserFile CarteVisiteFile { get; set; } = null;


    // New properties for Finances
    public string TpsInput { get; set; } = string.Empty;
    public string TvqInput { get; set; } = string.Empty;
    public string ConditionPaiement { get; set; } = "Dans les 30 jours sans déduction";
    public string Devise { get; set; } = "CAD";
    public string ModeCom { get; set; } = "Courriel";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private NavigationManager Navigation;



    [Parameter]
    public EventCallback UploadSuccessful { get; set; }

    public string EtatDemande { get; set; }
    //*******************************************SET FOR INFOS MODIFY***********************************
    public async Task FetchUser(int id, CancellationToken token)
    {
        var fournisseur = await _dbContext.Fournisseurs
                    .Include(f => f.Adresses)
                    .Include(f => f.Contacts)
                    .Include(f => f.Finances)
                    .Include(f => f.Produitsservices)
                        .ThenInclude(p => p.Comodite)  // Including Comodite
                    .Include(f => f.Licencesrbqs)
                        .ThenInclude(l => l.SouscategorieLicencerbqs)  // Ensure SouscategorieLicencerbqs is included here
                    .Include(f => f.Brochures)
                    .FirstOrDefaultAsync(f => f.FournisseurId == id);

        if (fournisseur == null)
        {
            // Handle case where fournisseur is not found
            Console.WriteLine("Fournisseur not found.");
            return;
        }


        //SET FOURNISSEUR
        NomEntrepriseInput = fournisseur.NomEntreprise;
        NeqInput = fournisseur.Neq;
        EmailInput = fournisseur.CourrielEntreprise;
        PasswordInput = string.Empty;
        RepeatPasswordInput = string.Empty;

        //SET ETAT DEMANDE POUR VOIR SI SHOW FINANCE
        EtatDemande = fournisseur.EtatDemande;

        //SET ADRESSE
        ICollection<Adress> adresses = fournisseur.Adresses;
        Adress adresse = adresses.SingleOrDefault();
        NumCiviqueInput = adresse.NumeroCivique;
        BureauInput = adresse.Bureau;
        RueInput = adresse.Rue;
        if (!string.IsNullOrEmpty(adresse.CodeMunicipalite))
            MunicipaliteInput = adresse.CodeMunicipalite;
        else
            NomMunicipaliteInput = adresse.NomMunicipalite;
        ProvinceInput = adresse.CodeProvince;
        CodePostalInput = adresse.CodePostal;
        NumeroTelephoneInput = adresse.NumTel;
        NumeroPosteInput = adresse.Poste;
        SiteWebInput = fournisseur.SiteWeb;

        //SET CONTACTS 
        ContactsInput.Clear();
        List<Contact> Contacts = fournisseur.Contacts.ToList();
        foreach (Contact contact in Contacts)
        {
            ContactInput toAdd = new ContactInput
            {
                Prenom = contact.PrenomContact,
                Nom = contact.NomContact,
                Role = contact.FonctionContact,
                Email = contact.CourrielContact,
                NumeroTelephone = contact.NumTelContact,
                Poste = contact.PosteTelContact,
                TypeTelephone = contact.TypeTel,

                PrenomError = string.Empty,
                NomError = string.Empty,
                RoleError = string.Empty,
                EmailError = string.Empty,
                NumeroTelephoneError = string.Empty,
                PosteError = string.Empty,
                TypeTelephoneError = string.Empty
            };

            ContactsInput.Add(toAdd);
        }

        //SET PRODUITS
        DescriptionProduitsServicesInput = fournisseur.DetailsEntreprise;
        List<Produitsservice> produits = fournisseur.Produitsservices.ToList();

        ProduitsServicesSelectionnesInput.Clear();

        foreach (Produitsservice produit in produits)
        {
            UnspscComodite comodite = produit.Comodite;
            ProduitsServicesSelectionnesInput.Add(comodite);
        }

        //SET LICENCE RBQ
        var licence = fournisseur.Licencesrbqs.SingleOrDefault();
        RBQNumberInput = licence.NumLicence;
        SelectedStatus = licence.StatutLicence;
        SelectedLicenseType = licence.TypeLicence;
        SelectedCategory = licence.Categorie;
        List<SouscategorieLicencerbq> souscategories = licence.SouscategorieLicencerbqs.ToList();

        SelectedSubCategories.Clear();
        foreach (SouscategorieLicencerbq souscategorie in souscategories)
        {
            var entry = _dbContext.Souscategoriesafter2008s.Where(s => s.SousCategorieAfter2008Id == souscategorie.IdSousCategorie).FirstOrDefault();
            SelectedSubCategories.Add(entry);
        }

        //SET BROCHURES 
        List<Brochure> brochures = fournisseur.Brochures.ToList();

        foreach (Brochure brochure in brochures)
        {
            if (brochure.NoFichier == "Brochure")
            {
                SelectedBrochure = brochure;
                OriginalBrochureName = brochure.NomFichier;
            }
            if (brochure.NoFichier == "Carte Affaire")
            {
                SelectedCarteAffaire = brochure;
                OriginalCarteAffaireName = brochure.NomFichier;
            }
        }


        //SET FINANCES
        var finances = fournisseur.Finances.SingleOrDefault();
        if (finances != null)
        {
            TpsInput = finances.Tps;
            TvqInput = finances.Tvq;
            ConditionPaiement = finances.CodeConditionPaiement;
            Devise = finances.Devise;
            ModeCom = finances.ModeCom;
        }

    }
    //*****************************************MODIFY FORM************************************************
    public async Task ModifyDataAsync( int? id)
    {
        var fournisseur = await _dbContext.Fournisseurs
            .Include(f => f.Adresses)
            .Include(f => f.Contacts)
            .Include(f => f.Finances)
            .Include(f => f.Produitsservices)
                .ThenInclude(p => p.Comodite)  // Including Comodite
            .Include(f => f.Licencesrbqs)
                .ThenInclude(l => l.SouscategorieLicencerbqs)  // Ensure SouscategorieLicencerbqs is included here
            .Include(f => f.Brochures)
            .FirstOrDefaultAsync(f => f.FournisseurId == id);

        if (fournisseur == null)
        {
            // Handle case where fournisseur is not found
            Console.WriteLine("Fournisseur not found.");
            return;
        }

        // You can now modify the fournisseur or use it for further processing


        //MODIFY FOURNISSEUR
        fournisseur.NomEntreprise = NomEntrepriseInput;
        fournisseur.Neq = NeqInput;
        fournisseur.CourrielEntreprise = EmailInput;
        fournisseur.DetailsEntreprise = DescriptionProduitsServicesInput;
        fournisseur.SiteWeb = SiteWebInput;

        await _dbContext.SaveChangesAsync();

        //MODIFY ADRESSE
        string cleanedCodePostal = Regex.Replace(CodePostalInput, @"\s+", "");
        string cleanedNumeroTelephone = Regex.Replace(NumeroTelephoneInput, @"-", "");
        Console.WriteLine(MunicipaliteInput + "**********************************");

        Adress adresse = fournisseur.Adresses.SingleOrDefault();
        adresse.NumeroCivique = NumCiviqueInput;
        adresse.Rue = RueInput;
        adresse.Bureau = BureauInput;
        if (!string.IsNullOrEmpty(MunicipaliteInput))
            adresse.CodeMunicipalite = MunicipaliteInput;
        else
            adresse.NomMunicipalite = NomMunicipaliteInput;
        adresse.CodeProvince = ProvinceInput;
        adresse.CodePostal = cleanedCodePostal;
        adresse.NumTel = cleanedNumeroTelephone;
        adresse.Poste = NumeroPosteInput;

        await _dbContext.SaveChangesAsync();

        //MODIFY CONTACTS
        List<Contact> contacts = fournisseur.Contacts.ToList();

        if (contacts.Count == ContactsInput.Count)
        {
            int index = 0;
            foreach (Contact contact in contacts)
            {
                string cleanedTelephone = Regex.Replace(ContactsInput[index].NumeroTelephone, @"-", "");
                contact.PrenomContact = ContactsInput[index].Prenom;
                contact.NomContact = ContactsInput[index].Nom;
                contact.FonctionContact = ContactsInput[index].Role;
                contact.CourrielContact = ContactsInput[index].Email;
                contact.TypeTel = ContactsInput[index].TypeTelephone;
                contact.NumTelContact = cleanedTelephone;
                contact.PosteTelContact = ContactsInput[index].Poste;
                index++;
            }
        }
        else if (contacts.Count < ContactsInput.Count)
        {
            int currentContacts = contacts.Count;
            int newContacts = ContactsInput.Count;
            int toCreate = newContacts - currentContacts;

            for (int i = 0; i < currentContacts + toCreate; i++)
            {
                if (i < currentContacts)
                {
                    string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                    contacts[i].PrenomContact = ContactsInput[i].Prenom;
                    contacts[i].NomContact = ContactsInput[i].Nom;
                    contacts[i].FonctionContact = ContactsInput[i].Role;
                    contacts[i].CourrielContact = ContactsInput[i].Email;
                    contacts[i].TypeTel = ContactsInput[i].TypeTelephone;
                    contacts[i].NumTelContact = cleanedTelephone;
                    contacts[i].PosteTelContact = ContactsInput[i].Poste;
                }
                else
                {
                    string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                    var contact = new Contact
                    {
                        PrenomContact = ContactsInput[i].Prenom,
                        NomContact = ContactsInput[i].Nom,
                        FonctionContact = ContactsInput[i].Role,
                        CourrielContact = ContactsInput[i].Email,
                        TypeTel = ContactsInput[i].TypeTelephone,
                        NumTelContact = cleanedTelephone,
                        PosteTelContact = ContactsInput[i].Poste,
                        FournisseurId = fournisseur.FournisseurId
                    };
                    _dbContext.Add(contact);
                }
            }

        }
        else if (contacts.Count > ContactsInput.Count)
        {
            int currentContacts = contacts.Count;
            int newContacts = ContactsInput.Count;
            int toDelete = currentContacts - newContacts;

            for (int i = 0; i < currentContacts; i++)
            {
                if (i < currentContacts - toDelete)
                {
                    string cleanedTelephone = Regex.Replace(ContactsInput[i].NumeroTelephone, @"-", "");
                    contacts[i].PrenomContact = ContactsInput[i].Prenom;
                    contacts[i].NomContact = ContactsInput[i].Nom;
                    contacts[i].FonctionContact = ContactsInput[i].Role;
                    contacts[i].CourrielContact = ContactsInput[i].Email;
                    contacts[i].TypeTel = ContactsInput[i].TypeTelephone;
                    contacts[i].NumTelContact = cleanedTelephone;
                    contacts[i].PosteTelContact = ContactsInput[i].Poste;
                }
                else
                {
                    _dbContext.Remove(contacts[i]);
                }
            }
        }

        await _dbContext.SaveChangesAsync();

        //MODIFY PRODUITS
        List<Produitsservice> produits = fournisseur.Produitsservices.ToList();

        if (produits.Count == ProduitsServicesSelectionnesInput.Count)
        {
            int index = 0;
            foreach (Produitsservice produit in produits)
            {
                produit.ComoditeId = ProduitsServicesSelectionnesInput[index].ComoditeId;
                index++;
            }
        }
        else if (produits.Count < ProduitsServicesSelectionnesInput.Count)
        {
            int currentProduits = produits.Count;
            int newProduits = ProduitsServicesSelectionnesInput.Count;
            int toCreate = newProduits - currentProduits;

            for (int i = 0; i < currentProduits + toCreate; i++)
            {
                if (i < currentProduits)
                {
                    produits[i].ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId;
                }
                else
                {
                    var produit = new Produitsservice
                    {
                        ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId,
                        FournisseurId = fournisseur.FournisseurId
                    };
                    _dbContext.Add(produit);
                }
            }
        }
        else if (produits.Count > ProduitsServicesSelectionnesInput.Count)
        {
            int currentProduits = produits.Count;
            int newProduits = ProduitsServicesSelectionnesInput.Count;
            int toDelete = currentProduits - newProduits;

            for (int i = 0; i < currentProduits; i++)
            {
                if (i < currentProduits - toDelete)
                {
                    produits[i].ComoditeId = ProduitsServicesSelectionnesInput[i].ComoditeId;
                }
                else
                {
                    _dbContext.Remove(produits[i]);
                }
            }
        }
        await _dbContext.SaveChangesAsync();

        //MODIFY RBQ
        Licencesrbq licence = fournisseur.Licencesrbqs.SingleOrDefault();
        string cleanedLicence = Regex.Replace(RBQNumberInput, @"-", "");
        licence.NumLicence = cleanedLicence;
        licence.StatutLicence = SelectedStatus;
        licence.TypeLicence = SelectedLicenseType;
        licence.Categorie = SelectedCategory;

        List<SouscategorieLicencerbq> sousCategories = licence.SouscategorieLicencerbqs.ToList();


        if (sousCategories.Count == SelectedSubCategories.Count)
        {
            int index = 0;
            foreach (SouscategorieLicencerbq liaison in sousCategories)
            {
                liaison.IdSousCategorie = SelectedSubCategories[index].SousCategorieAfter2008Id;
                index++;
            }
        }
        else if (sousCategories.Count < SelectedSubCategories.Count)
        {
            int currentSubs = sousCategories.Count;
            int newSubs = SelectedSubCategories.Count;
            int toCreate = newSubs - currentSubs;

            for (int i = 0; i < currentSubs + toCreate; i++)
            {
                if (i < currentSubs)
                {
                    sousCategories[i].IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id;
                }
                else
                {
                    var sub = new SouscategorieLicencerbq
                    {
                        IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id,
                        IdLicence = licence.RbqId
                    };
                    _dbContext.Add(sub);
                }
            }
        }
        else if (sousCategories.Count > SelectedSubCategories.Count)
        {
            int currentSubs = sousCategories.Count;
            int newSubs = SelectedSubCategories.Count;
            int toDelete = currentSubs - newSubs;

            for (int i = 0; i < currentSubs; i++)
            {
                if (i < currentSubs - toDelete)
                {
                    sousCategories[i].IdSousCategorie = SelectedSubCategories[i].SousCategorieAfter2008Id;
                }
                else
                {
                    _dbContext.Remove(sousCategories[i]);
                }
            }
        }
        await _dbContext.SaveChangesAsync();

        //MODIFY BROCHURE

        List<Brochure> brochures = fournisseur.Brochures.ToList();

        if (brochures.Count == 2)
        {
            foreach (Brochure thisBrochure in brochures)
            {
                if (thisBrochure.NoFichier == "Brochure")
                {
                    if (BrochureFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedBrochure.LienDocument);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                        }
                        thisBrochure.LienDocument = SelectedBrochure.LienDocument;
                        thisBrochure.NomFichier = SelectedBrochure.NomFichier;
                        thisBrochure.Taille = SelectedBrochure.Taille;
                        thisBrochure.TypeFichier = SelectedBrochure.TypeFichier;
                    }
                }
                else
                {
                    if (CarteVisiteFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedCarteAffaire.LienDocument);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                        }
                        thisBrochure.LienDocument = SelectedCarteAffaire.LienDocument;
                        thisBrochure.NomFichier = SelectedCarteAffaire.NomFichier;
                        thisBrochure.Taille = SelectedCarteAffaire.Taille;
                        thisBrochure.TypeFichier = SelectedCarteAffaire.TypeFichier;
                    }
                }
            }
        }

        if (brochures.Count == 1)
        {
            if (brochures[0].NoFichier == "Brochure")
            {
                if (BrochureFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedBrochure.LienDocument);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                    }
                    brochures[0].LienDocument = SelectedBrochure.LienDocument;
                    brochures[0].NomFichier = SelectedBrochure.NomFichier;
                    brochures[0].Taille = SelectedBrochure.Taille;
                    brochures[0].TypeFichier = SelectedBrochure.TypeFichier;
                }
                if (CarteVisiteFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedCarteAffaire.LienDocument);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                    }
                    SelectedCarteAffaire.FournisseurId = fournisseur.FournisseurId;
                    _dbContext.Brochures.Add(SelectedCarteAffaire);
                }
            }
            else
            {
                if (CarteVisiteFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedCarteAffaire.LienDocument);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                    }
                    brochures[0].LienDocument = SelectedCarteAffaire.LienDocument;
                    brochures[0].NomFichier = SelectedCarteAffaire.NomFichier;
                    brochures[0].Taille = SelectedCarteAffaire.Taille;
                    brochures[0].TypeFichier = SelectedCarteAffaire.TypeFichier;
                }
                if (BrochureFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedBrochure.LienDocument);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                    }
                    SelectedBrochure.FournisseurId = fournisseur.FournisseurId;
                    _dbContext.Brochures.Add(SelectedBrochure);
                }
            }
        }

        if (brochures.Count == 0)
        {
            if (BrochureFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedBrochure.LienDocument);
                Console.WriteLine("**************************************************" + path);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await BrochureFile.OpenReadStream(75000000).CopyToAsync(stream);
                }
                SelectedBrochure.FournisseurId = fournisseur.FournisseurId;
                _dbContext.Brochures.Add(SelectedBrochure);
            }
            if (CarteVisiteFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + SelectedCarteAffaire.LienDocument);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await CarteVisiteFile.OpenReadStream(75000000).CopyToAsync(stream);
                }
                SelectedCarteAffaire.FournisseurId = fournisseur.FournisseurId;
                _dbContext.Brochures.Add(SelectedCarteAffaire);
            }
        }


        await _dbContext.SaveChangesAsync();

        //MODIFT OR ADD FINANCES
        Finance finance = fournisseur.Finances.SingleOrDefault();

        if (fournisseur.EtatDemande == "Approuvée" && finance == null)
        {
            var newFinance = new Finance
            {
                Tvq = TvqInput,
                Tps = TpsInput,
                CodeConditionPaiement = ConditionPaiement,
                Devise = Devise,
                ModeCom = ModeCom,
                FournisseurId = fournisseur.FournisseurId
            };
            _dbContext.Add(newFinance);
        }
        else if (fournisseur.EtatDemande == "Approuvée" && finance != null)
        {
            finance.Tvq = TvqInput;
            finance.Tps = TpsInput;
            finance.CodeConditionPaiement = ConditionPaiement;
            finance.Devise = Devise;
            finance.ModeCom = ModeCom;
        }

        await _dbContext.SaveChangesAsync();

        Navigation.NavigateTo("/profil?success=true");
    }


    private string ComputeMd5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convertir le tableau de bytes en une chaîne hexadécimale
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }


}


public class ContactInput
{
    public string Prenom { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NumeroTelephone { get; set; } = string.Empty;
    public string TypeTelephone { get; set; } = string.Empty;
    public string Poste { get; set; } = string.Empty;

    // Error messages
    public string PrenomError { get; set; } = string.Empty;
    public string NomError { get; set; } = string.Empty;
    public string RoleError { get; set; } = string.Empty;
    public string EmailError { get; set; } = string.Empty;
    public string NumeroTelephoneError { get; set; } = string.Empty;
    public string TypeTelephoneError { get; set; } = string.Empty;
    public string PosteError { get; set; } = string.Empty;
}



