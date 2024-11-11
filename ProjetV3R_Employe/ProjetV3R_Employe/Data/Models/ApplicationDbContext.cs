using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace IntegrationV3R_PortailFournisseur.Data.Models;

public partial class ApplicationDbContext : DbContext
{

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
