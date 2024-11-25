using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetV3R_Employe.Data.Models.ProjetV3R;

public partial class ApplicationDbContext2 : DbContext
{
    public ApplicationDbContext2()
    {
    }

    public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Brochure> Brochures { get; set; }

    public virtual DbSet<Conditionspaiement> Conditionspaiements { get; set; }

    public virtual DbSet<Connexion> Connexions { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Licencesrbq> Licencesrbqs { get; set; }

    public virtual DbSet<Motsdepass> Motsdepasses { get; set; }

    public virtual DbSet<Municipalite> Municipalites { get; set; }

    public virtual DbSet<Produitsservice> Produitsservices { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Regionadministrative> Regionadministratives { get; set; }

    public virtual DbSet<SeaoUnspscCategory> SeaoUnspscCategories { get; set; }

    public virtual DbSet<SeaoUnspscNature> SeaoUnspscNatures { get; set; }

    public virtual DbSet<SouscategorieLicencerbq> SouscategorieLicencerbqs { get; set; }

    public virtual DbSet<Souscategoriesafter2008> Souscategoriesafter2008s { get; set; }

    public virtual DbSet<Souscategoriesbefore2008> Souscategoriesbefore2008s { get; set; }

    public virtual DbSet<Territoire> Territoires { get; set; }

    public virtual DbSet<UnspscClass> UnspscClasses { get; set; }

    public virtual DbSet<UnspscComodite> UnspscComodites { get; set; }

    public virtual DbSet<UnspscFamille> UnspscFamilles { get; set; }

    public virtual DbSet<UnspscSegment> UnspscSegments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cours.cegep3r.info;port=3306;database=a2024_420517ri_gr2-eq1_1888838-louis-fernand-henri-folcher;user id=1888838;password=1888838", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.AdresseId).HasName("PRIMARY");

            entity.ToTable("adresses");

            entity.HasIndex(e => e.CodeMunicipalite, "codeMunicipalite");

            entity.HasIndex(e => e.CodeProvince, "codeProvince");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.AdresseId)
                .HasColumnType("int(11)")
                .HasColumnName("adresseId");
            entity.Property(e => e.Bureau)
                .HasMaxLength(2)
                .HasColumnName("bureau");
            entity.Property(e => e.CodeMunicipalite)
                .HasMaxLength(5)
                .HasColumnName("codeMunicipalite");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(6)
                .HasColumnName("codePostal");
            entity.Property(e => e.CodeProvince)
                .HasMaxLength(2)
                .HasColumnName("codeProvince");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NomMunicipalite)
                .HasMaxLength(64)
                .HasColumnName("nomMunicipalite");
            entity.Property(e => e.NumTel)
                .HasMaxLength(10)
                .HasColumnName("numTel");
            entity.Property(e => e.NumeroCivique)
                .HasMaxLength(5)
                .HasColumnName("numeroCivique");
            entity.Property(e => e.Poste)
                .HasMaxLength(5)
                .HasColumnName("poste");
            entity.Property(e => e.Rue)
                .HasMaxLength(64)
                .HasColumnName("rue");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");

            entity.HasOne(d => d.CodeMunicipaliteNavigation).WithMany(p => p.Adresses)
                .HasPrincipalKey(p => p.CodeMunicipalite)
                .HasForeignKey(d => d.CodeMunicipalite)
                .HasConstraintName("adresses_ibfk_2");

            entity.HasOne(d => d.CodeProvinceNavigation).WithMany(p => p.Adresses)
                .HasPrincipalKey(p => p.CodeProvince)
                .HasForeignKey(d => d.CodeProvince)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_ibfk_3");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adresses_ibfk_1");
        });

        modelBuilder.Entity<Brochure>(entity =>
        {
            entity.HasKey(e => e.BrochureId).HasName("PRIMARY");

            entity.ToTable("brochures");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.BrochureId)
                .HasColumnType("int(11)")
                .HasColumnName("brochureId");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.LienDocument)
                .HasMaxLength(128)
                .HasColumnName("lienDocument");
            entity.Property(e => e.NoFichier).HasMaxLength(32);
            entity.Property(e => e.NomFichier)
                .HasMaxLength(100)
                .HasColumnName("nomFichier");
            entity.Property(e => e.Taille)
                .HasColumnType("int(11)")
                .HasColumnName("taille");
            entity.Property(e => e.TypeFichier)
                .HasMaxLength(4)
                .HasColumnName("typeFichier");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Brochures)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("brochures_ibfk_1");
        });

        modelBuilder.Entity<Conditionspaiement>(entity =>
        {
            entity.HasKey(e => e.ConditionsPaiementsId).HasName("PRIMARY");

            entity.ToTable("conditionspaiements");

            entity.HasIndex(e => e.CodeConditionsPaiements, "codeConditionsPaiements").IsUnique();

            entity.HasIndex(e => e.NomConditionsPaiements, "nomConditionsPaiements").IsUnique();

            entity.Property(e => e.ConditionsPaiementsId)
                .HasColumnType("int(11)")
                .HasColumnName("conditionsPaiementsId");
            entity.Property(e => e.CodeConditionsPaiements)
                .HasMaxLength(8)
                .HasColumnName("codeConditionsPaiements");
            entity.Property(e => e.NomConditionsPaiements)
                .HasMaxLength(64)
                .HasColumnName("nomConditionsPaiements");
        });

        modelBuilder.Entity<Connexion>(entity =>
        {
            entity.HasKey(e => e.ConnexionId).HasName("PRIMARY");

            entity.ToTable("connexions");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.ConnexionId)
                .HasColumnType("int(11)")
                .HasColumnName("connexionId");
            entity.Property(e => e.IpConnexion)
                .HasMaxLength(45)
                .HasColumnName("ipConnexion");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Connexions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("connexions_ibfk_1");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PRIMARY");

            entity.ToTable("contacts");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.ContactId).HasColumnType("int(11)");
            entity.Property(e => e.CourrielContact)
                .HasMaxLength(64)
                .HasColumnName("courrielContact");
            entity.Property(e => e.FonctionContact)
                .HasMaxLength(32)
                .HasColumnName("fonctionContact");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NomContact)
                .HasMaxLength(32)
                .HasColumnName("nomContact");
            entity.Property(e => e.NumTelContact)
                .HasMaxLength(10)
                .HasColumnName("numTelContact");
            entity.Property(e => e.PosteTelContact)
                .HasMaxLength(5)
                .HasColumnName("posteTelContact");
            entity.Property(e => e.PrenomContact)
                .HasMaxLength(32)
                .HasColumnName("prenomContact");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.TypeTel)
                .HasMaxLength(12)
                .HasColumnName("typeTel");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contacts_ibfk_1");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.HasKey(e => e.FinanceId).HasName("PRIMARY");

            entity.ToTable("finances");

            entity.HasIndex(e => e.CodeConditionPaiement, "codeConditionPaiement");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.HasIndex(e => e.Tps, "tps").IsUnique();

            entity.HasIndex(e => e.Tvq, "tvq").IsUnique();

            entity.Property(e => e.FinanceId)
                .HasColumnType("int(11)")
                .HasColumnName("financeId");
            entity.Property(e => e.CodeConditionPaiement)
                .HasMaxLength(4)
                .HasColumnName("codeConditionPaiement");
            entity.Property(e => e.Devise)
                .HasMaxLength(4)
                .HasColumnName("devise");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.ModeCom)
                .HasMaxLength(16)
                .HasColumnName("modeCom");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.Tps)
                .HasMaxLength(15)
                .HasColumnName("tps");
            entity.Property(e => e.Tvq)
                .HasMaxLength(16)
                .HasColumnName("tvq");

            entity.HasOne(d => d.CodeConditionPaiementNavigation).WithMany(p => p.Finances)
                .HasPrincipalKey(p => p.CodeConditionsPaiements)
                .HasForeignKey(d => d.CodeConditionPaiement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("finances_ibfk_2");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Finances)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("finances_ibfk_1");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.FournisseurId).HasName("PRIMARY");

            entity.ToTable("fournisseurs");

            entity.HasIndex(e => e.CourrielEntreprise, "courrielEntreprise").IsUnique();

            entity.HasIndex(e => e.Neq, "neq").IsUnique();

            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.CourrielEntreprise)
                .HasMaxLength(64)
                .HasColumnName("courrielEntreprise");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateInscription");
            entity.Property(e => e.DetailsEntreprise)
                .HasMaxLength(500)
                .HasColumnName("detailsEntreprise");
            entity.Property(e => e.EtatCompte)
                .IsRequired()
                .HasDefaultValueSql("false")
                .HasColumnName("etatCompte");
            entity.Property(e => e.EtatDemande)
                .HasMaxLength(32)
                .HasColumnName("etatDemande");
            entity.Property(e => e.Neq)
                .HasMaxLength(10)
                .HasColumnName("neq");
            entity.Property(e => e.NomEntreprise)
                .HasMaxLength(64)
                .HasColumnName("nomEntreprise");
            entity.Property(e => e.RaisonRefus)
                .HasMaxLength(500)
                .HasColumnName("raisonRefus");
            entity.Property(e => e.SiteWeb)
                .HasMaxLength(64)
                .HasColumnName("siteWeb");
        });

        modelBuilder.Entity<Licencesrbq>(entity =>
        {
            entity.HasKey(e => e.RbqId).HasName("PRIMARY");

            entity.ToTable("licencesrbq");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.HasIndex(e => e.NumLicence, "numLicence").IsUnique();

            entity.Property(e => e.RbqId)
                .HasColumnType("int(11)")
                .HasColumnName("rbqId");
            entity.Property(e => e.Categorie)
                .HasMaxLength(32)
                .HasColumnName("categorie");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.NumLicence)
                .HasMaxLength(10)
                .HasColumnName("numLicence");
            entity.Property(e => e.StatutLicence)
                .HasMaxLength(10)
                .HasColumnName("statutLicence");
            entity.Property(e => e.TypeLicence)
                .HasMaxLength(30)
                .HasColumnName("typeLicence");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Licencesrbqs)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("licencesrbq_ibfk_1");
        });

        modelBuilder.Entity<Motsdepass>(entity =>
        {
            entity.HasKey(e => e.MdpId).HasName("PRIMARY");

            entity.ToTable("motsdepasses");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.MdpId)
                .HasColumnType("int(11)")
                .HasColumnName("mdpId");
            entity.Property(e => e.IpChangementMdp)
                .HasMaxLength(45)
                .HasColumnName("ipChangementMdp");
            entity.Property(e => e.Mdp)
                .HasMaxLength(128)
                .HasColumnName("MDP");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Motsdepasses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("motsdepasses_ibfk_1");
        });

        modelBuilder.Entity<Municipalite>(entity =>
        {
            entity.HasKey(e => e.MunicipaliteId).HasName("PRIMARY");

            entity.ToTable("municipalites");

            entity.HasIndex(e => e.CodeMunicipalite, "codeMunicipalite").IsUnique();

            entity.HasIndex(e => e.CodeTerritoire, "codeTerritoire");

            entity.HasIndex(e => e.NomMunicipalite, "nomMunicipalite").IsUnique();

            entity.Property(e => e.MunicipaliteId)
                .HasColumnType("int(11)")
                .HasColumnName("municipaliteId");
            entity.Property(e => e.CodeMunicipalite)
                .HasMaxLength(5)
                .HasColumnName("codeMunicipalite");
            entity.Property(e => e.CodeTerritoire)
                .HasMaxLength(5)
                .HasColumnName("codeTerritoire");
            entity.Property(e => e.NomMunicipalite)
                .HasMaxLength(64)
                .HasColumnName("nomMunicipalite");

            entity.HasOne(d => d.CodeTerritoireNavigation).WithMany(p => p.Municipalites)
                .HasPrincipalKey(p => p.CodeTerritoire)
                .HasForeignKey(d => d.CodeTerritoire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("municipalites_ibfk_1");
        });

        modelBuilder.Entity<Produitsservice>(entity =>
        {
            entity.HasKey(e => e.ProduitId).HasName("PRIMARY");

            entity.ToTable("produitsservices");

            entity.HasIndex(e => e.ComoditeId, "comoditeId");

            entity.HasIndex(e => e.FournisseurId, "fournisseurId");

            entity.Property(e => e.ProduitId)
                .HasColumnType("int(11)")
                .HasColumnName("produitId");
            entity.Property(e => e.ComoditeId)
                .HasColumnType("int(11)")
                .HasColumnName("comoditeId");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurId");
            entity.Property(e => e.Timestamps)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamps");

            entity.HasOne(d => d.Comodite).WithMany(p => p.Produitsservices)
                .HasForeignKey(d => d.ComoditeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("produitsservices_ibfk_2");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Produitsservices)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("produitsservices_ibfk_1");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PRIMARY");

            entity.ToTable("provinces");

            entity.HasIndex(e => e.CodeProvince, "codeProvince").IsUnique();

            entity.HasIndex(e => e.NomProvince, "nomProvince").IsUnique();

            entity.Property(e => e.ProvinceId)
                .HasColumnType("int(11)")
                .HasColumnName("provinceId");
            entity.Property(e => e.CodeProvince)
                .HasMaxLength(2)
                .HasColumnName("codeProvince");
            entity.Property(e => e.NomProvince)
                .HasMaxLength(30)
                .HasColumnName("nomProvince");
        });

        modelBuilder.Entity<Regionadministrative>(entity =>
        {
            entity.HasKey(e => e.RegionAdminId).HasName("PRIMARY");

            entity.ToTable("regionadministratives");

            entity.HasIndex(e => e.CodeRegionAdministrative, "codeRegionAdministrative").IsUnique();

            entity.HasIndex(e => e.NomRegionAmdin, "nomRegionAmdin").IsUnique();

            entity.Property(e => e.RegionAdminId)
                .HasColumnType("int(11)")
                .HasColumnName("regionAdminId");
            entity.Property(e => e.CodeRegionAdministrative)
                .HasMaxLength(2)
                .HasColumnName("codeRegionAdministrative");
            entity.Property(e => e.NomRegionAmdin)
                .HasMaxLength(32)
                .HasColumnName("nomRegionAmdin");
        });

        modelBuilder.Entity<SeaoUnspscCategory>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PRIMARY");

            entity.ToTable("seao_unspsc_categories");

            entity.HasIndex(e => e.CategorieCode, "categorieCode").IsUnique();

            entity.HasIndex(e => e.CategorieNom, "categorieNom").IsUnique();

            entity.HasIndex(e => e.NatureCode, "natureCode");

            entity.Property(e => e.CategorieId)
                .HasColumnType("int(11)")
                .HasColumnName("categorieId");
            entity.Property(e => e.CategorieCode)
                .HasMaxLength(3)
                .HasColumnName("categorieCode");
            entity.Property(e => e.CategorieNom).HasColumnName("categorieNom");
            entity.Property(e => e.NatureCode)
                .HasMaxLength(3)
                .HasColumnName("natureCode");

            entity.HasOne(d => d.NatureCodeNavigation).WithMany(p => p.SeaoUnspscCategories)
                .HasPrincipalKey(p => p.NatureCode)
                .HasForeignKey(d => d.NatureCode)
                .HasConstraintName("seao_unspsc_categories_ibfk_1");
        });

        modelBuilder.Entity<SeaoUnspscNature>(entity =>
        {
            entity.HasKey(e => e.NatureId).HasName("PRIMARY");

            entity.ToTable("seao_unspsc_natures");

            entity.HasIndex(e => e.NatureCode, "natureCode").IsUnique();

            entity.HasIndex(e => e.NatureNom, "natureNom").IsUnique();

            entity.Property(e => e.NatureId)
                .HasColumnType("int(11)")
                .HasColumnName("natureId");
            entity.Property(e => e.NatureCode)
                .HasMaxLength(3)
                .HasColumnName("natureCode");
            entity.Property(e => e.NatureNom)
                .HasMaxLength(30)
                .HasColumnName("natureNom");
        });

        modelBuilder.Entity<SouscategorieLicencerbq>(entity =>
        {
            entity.HasKey(e => e.SousCategrorieRbqId).HasName("PRIMARY");

            entity.ToTable("souscategorie_licencerbq");

            entity.HasIndex(e => e.IdLicence, "idLicence");

            entity.HasIndex(e => e.IdSousCategorie, "idSousCategorie");

            entity.Property(e => e.SousCategrorieRbqId)
                .HasColumnType("int(11)")
                .HasColumnName("sousCategrorie_rbqId");
            entity.Property(e => e.IdLicence)
                .HasColumnType("int(11)")
                .HasColumnName("idLicence");
            entity.Property(e => e.IdSousCategorie)
                .HasColumnType("int(11)")
                .HasColumnName("idSousCategorie");

            entity.HasOne(d => d.IdLicenceNavigation).WithMany(p => p.SouscategorieLicencerbqs)
                .HasForeignKey(d => d.IdLicence)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("souscategorie_licencerbq_ibfk_1");

            entity.HasOne(d => d.IdSousCategorieNavigation).WithMany(p => p.SouscategorieLicencerbqs)
                .HasForeignKey(d => d.IdSousCategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("souscategorie_licencerbq_ibfk_2");
        });

        modelBuilder.Entity<Souscategoriesafter2008>(entity =>
        {
            entity.HasKey(e => e.SousCategorieAfter2008Id).HasName("PRIMARY");

            entity.ToTable("souscategoriesafter2008");

            entity.HasIndex(e => e.NomSousCategorieAfter2008, "nomSousCategorieAfter2008").IsUnique();

            entity.HasIndex(e => e.NumeroSousCategorieAfter2008, "numeroSousCategorieAfter2008").IsUnique();

            entity.Property(e => e.SousCategorieAfter2008Id).HasColumnType("int(11)");
            entity.Property(e => e.NomSousCategorieAfter2008)
                .HasMaxLength(128)
                .HasColumnName("nomSousCategorieAfter2008");
            entity.Property(e => e.NumeroSousCategorieAfter2008)
                .HasMaxLength(8)
                .HasColumnName("numeroSousCategorieAfter2008");
        });

        modelBuilder.Entity<Souscategoriesbefore2008>(entity =>
        {
            entity.HasKey(e => e.SousCategorieBefore2008Id).HasName("PRIMARY");

            entity.ToTable("souscategoriesbefore2008");

            entity.HasIndex(e => e.NomSousCategorieBefore2008, "nomSousCategorieBefore2008").IsUnique();

            entity.HasIndex(e => e.NumeroSousCategorieBefore2008, "numeroSousCategorieBefore2008").IsUnique();

            entity.Property(e => e.SousCategorieBefore2008Id).HasColumnType("int(11)");
            entity.Property(e => e.NomSousCategorieBefore2008)
                .HasMaxLength(128)
                .HasColumnName("nomSousCategorieBefore2008");
            entity.Property(e => e.NumeroSousCategorieBefore2008)
                .HasMaxLength(8)
                .HasColumnName("numeroSousCategorieBefore2008");
        });

        modelBuilder.Entity<Territoire>(entity =>
        {
            entity.HasKey(e => e.TerritoireId).HasName("PRIMARY");

            entity.ToTable("territoires");

            entity.HasIndex(e => e.CodeRegionAdministrative, "codeRegionAdministrative");

            entity.HasIndex(e => e.CodeTerritoire, "codeTerritoire").IsUnique();

            entity.HasIndex(e => e.NomTerritoire, "nomTerritoire").IsUnique();

            entity.Property(e => e.TerritoireId)
                .HasColumnType("int(11)")
                .HasColumnName("territoireId");
            entity.Property(e => e.CodeRegionAdministrative)
                .HasMaxLength(2)
                .HasColumnName("codeRegionAdministrative");
            entity.Property(e => e.CodeTerritoire)
                .HasMaxLength(5)
                .HasColumnName("codeTerritoire");
            entity.Property(e => e.NomTerritoire)
                .HasMaxLength(64)
                .HasColumnName("nomTerritoire");

            entity.HasOne(d => d.CodeRegionAdministrativeNavigation).WithMany(p => p.Territoires)
                .HasPrincipalKey(p => p.CodeRegionAdministrative)
                .HasForeignKey(d => d.CodeRegionAdministrative)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("territoires_ibfk_1");
        });

        modelBuilder.Entity<UnspscClass>(entity =>
        {
            entity.HasKey(e => e.ClasseId).HasName("PRIMARY");

            entity.ToTable("unspsc_classes");

            entity.HasIndex(e => e.ClasseNombre, "classeNombre").IsUnique();

            entity.HasIndex(e => e.FamilleNombre, "familleNombre");

            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classeId");
            entity.Property(e => e.ClasseNombre)
                .HasMaxLength(8)
                .HasColumnName("classeNombre");
            entity.Property(e => e.ClasseTitreEn)
                .HasMaxLength(64)
                .HasColumnName("classeTitreEn");
            entity.Property(e => e.ClasseTitreFr)
                .HasMaxLength(64)
                .HasColumnName("classeTitreFr");
            entity.Property(e => e.FamilleNombre)
                .HasMaxLength(8)
                .HasColumnName("familleNombre");

            entity.HasOne(d => d.FamilleNombreNavigation).WithMany(p => p.UnspscClasses)
                .HasPrincipalKey(p => p.FamilleNombre)
                .HasForeignKey(d => d.FamilleNombre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unspsc_classes_ibfk_1");
        });

        modelBuilder.Entity<UnspscComodite>(entity =>
        {
            entity.HasKey(e => e.ComoditeId).HasName("PRIMARY");

            entity.ToTable("unspsc_comodites");

            entity.HasIndex(e => e.CategorieSeao, "categorieSEAO");

            entity.HasIndex(e => e.ClasseNombre, "classeNombre");

            entity.HasIndex(e => e.ComoditeNombre, "comoditeNombre").IsUnique();

            entity.Property(e => e.ComoditeId)
                .HasColumnType("int(11)")
                .HasColumnName("comoditeId");
            entity.Property(e => e.CategorieSeao)
                .HasMaxLength(3)
                .HasColumnName("categorieSEAO");
            entity.Property(e => e.ClasseNombre)
                .HasMaxLength(8)
                .HasColumnName("classeNombre");
            entity.Property(e => e.ComoditeNombre)
                .HasMaxLength(8)
                .HasColumnName("comoditeNombre");
            entity.Property(e => e.ComoditeTitreEn)
                .HasMaxLength(64)
                .HasColumnName("comoditeTitreEn");
            entity.Property(e => e.ComoditeTitreFr)
                .HasMaxLength(64)
                .HasColumnName("comoditeTitreFr");

            entity.HasOne(d => d.CategorieSeaoNavigation).WithMany(p => p.UnspscComodites)
                .HasPrincipalKey(p => p.CategorieCode)
                .HasForeignKey(d => d.CategorieSeao)
                .HasConstraintName("unspsc_comodites_ibfk_2");

            entity.HasOne(d => d.ClasseNombreNavigation).WithMany(p => p.UnspscComodites)
                .HasPrincipalKey(p => p.ClasseNombre)
                .HasForeignKey(d => d.ClasseNombre)
                .HasConstraintName("unspsc_comodites_ibfk_1");
        });

        modelBuilder.Entity<UnspscFamille>(entity =>
        {
            entity.HasKey(e => e.FamilleId).HasName("PRIMARY");

            entity.ToTable("unspsc_familles");

            entity.HasIndex(e => e.FamilleNombre, "familleNombre").IsUnique();

            entity.HasIndex(e => e.SegmentNombre, "segmentNombre");

            entity.Property(e => e.FamilleId)
                .HasColumnType("int(11)")
                .HasColumnName("familleId");
            entity.Property(e => e.FamilleNombre)
                .HasMaxLength(8)
                .HasColumnName("familleNombre");
            entity.Property(e => e.FamilleTitreEn)
                .HasMaxLength(64)
                .HasColumnName("familleTitreEn");
            entity.Property(e => e.FamilleTitreFr)
                .HasMaxLength(64)
                .HasColumnName("familleTitreFr");
            entity.Property(e => e.SegmentNombre)
                .HasMaxLength(8)
                .HasColumnName("segmentNombre");

            entity.HasOne(d => d.SegmentNombreNavigation).WithMany(p => p.UnspscFamilles)
                .HasPrincipalKey(p => p.SegmentNombre)
                .HasForeignKey(d => d.SegmentNombre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unspsc_familles_ibfk_1");
        });

        modelBuilder.Entity<UnspscSegment>(entity =>
        {
            entity.HasKey(e => e.SegmentId).HasName("PRIMARY");

            entity.ToTable("unspsc_segments");

            entity.HasIndex(e => e.SegmentNombre, "segmentNombre").IsUnique();

            entity.Property(e => e.SegmentId)
                .HasColumnType("int(11)")
                .HasColumnName("segmentId");
            entity.Property(e => e.SegmentNombre)
                .HasMaxLength(8)
                .HasColumnName("segmentNombre");
            entity.Property(e => e.SegmentTitreEn)
                .HasMaxLength(64)
                .HasColumnName("segmentTitreEn");
            entity.Property(e => e.SegmentTitreFr)
                .HasMaxLength(64)
                .HasColumnName("segmentTitreFr");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.FournisseurId, "fournisseurID");

            entity.HasIndex(e => e.Identifiant, "identifiant").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.FournisseurId)
                .HasColumnType("int(11)")
                .HasColumnName("fournisseurID");
            entity.Property(e => e.Identifiant)
                .HasMaxLength(64)
                .HasColumnName("identifiant");

            entity.HasOne(d => d.Fournisseur).WithMany(p => p.Users)
                .HasForeignKey(d => d.FournisseurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
