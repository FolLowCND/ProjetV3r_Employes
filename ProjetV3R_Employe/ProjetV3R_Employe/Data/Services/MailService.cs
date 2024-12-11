using ProjetV3R_Employe.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MySqlConnector;


public class MailService
{
    private readonly ApplicationDbContext _dbContext;

    public MailService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AjouterEmailAsync(string titreEmail, string objetEmail, string fromEmail, string ccEmail, string bodyEmail)
    {
        try
        {
            var email = new Email
            {
                TitreEmail = titreEmail,
                ObjetEmail = objetEmail,
                FromEmail = fromEmail,
                CcEmail = ccEmail,
                BodyEmail = bodyEmail
            };

            _dbContext.Emails.Add(email);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de l'ajout de l'email : {ex.Message}");
        }
    }


    public async Task ModifierEmailAsync(int id, string titreEmail, string objetEmail, string fromEmail, string ccEmail, string bodyEmail)
    {
        try
        {
            var emailExistant = await _dbContext.Emails.FirstOrDefaultAsync(e => e.IdEmail == id);

            if (emailExistant == null)
            {
                throw new Exception("L'email à modifier n'existe pas.");
            }

            emailExistant.TitreEmail = titreEmail;
            emailExistant.ObjetEmail = objetEmail;
            emailExistant.FromEmail = fromEmail;
            emailExistant.CcEmail = ccEmail;
            emailExistant.BodyEmail = bodyEmail;

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la modification de l'email : {ex.Message}");
        }
    }


    public void EnvoyerEmail(string toEmail, string fromEmail, string sujet, string corps, List<string> ccEmails = null)
    {
        Console.WriteLine($"Envoi de l'email:\nÀ: {toEmail}\nDe: {fromEmail}\nSujet: {sujet}\nCorps: {corps}");

        if (ccEmails != null && ccEmails.Count > 0)
        {
            Console.WriteLine("CC: " + string.Join(", ", ccEmails));
        }
    }


    public async Task<Email> ObtenirEmailParIdAsync(int id)
    {
        try
        {
            var email = await _dbContext.Emails.FirstOrDefaultAsync(e => e.IdEmail == id);

            if (email == null)
            {
                throw new Exception("L'email demandé n'existe pas.");
            }

            return email;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération de l'email : {ex.Message}");
        }
    }


    public async Task SupprimerEmailAsync(int id)
    {
        try
        {
            var email = await _dbContext.Emails.FirstOrDefaultAsync(e => e.IdEmail == id);

            if (email == null)
            {
                throw new Exception("L'email à supprimer n'existe pas.");
            }

            _dbContext.Emails.Remove(email);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la suppression de l'email : {ex.Message}");
        }
    }

    public async Task<List<Email>> ObtenirTousLesEmailsAsync()
    {
        try
        {
            return await _dbContext.Emails.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération des emails : {ex.Message}");
        }
    }
}
