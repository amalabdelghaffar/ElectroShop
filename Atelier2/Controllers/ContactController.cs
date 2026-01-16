using Atelier2.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    // ... tes autres actions

    [HttpGet]
    public IActionResult Contact()
    {
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Contact(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // On ré-affiche le formulaire avec les messages d’erreur
            return View(model);
        }

        // Ici tu pourrais envoyer un mail plus tard.
        // Pour l’instant on reste "statique" : juste un message de confirmation.
        TempData["ContactSuccess"] = "Merci ! Votre message a bien été envoyé. Nous vous répondrons dans les plus brefs délais.";

        return RedirectToAction(nameof(Contact));
    }
}
