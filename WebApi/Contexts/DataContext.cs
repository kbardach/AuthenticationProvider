using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Contexts;

// Denna ska ärva från IdentityDbContext
// Generera en constructor med options, lägg till <DataContext> -> Use primary constructor -> Reg. program.cs
public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
    // Det här är för att skapa en ny tabell utifrån AccountVerificationEntity.
    // När Entiteten är skapad & Reg. här så kör Add-Migration + Update-Database för att skapa en tabell i databasen.
    public DbSet<AccountVerficationEntity> AccountVerfications { get; set; }

}
