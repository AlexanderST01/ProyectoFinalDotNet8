using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProyectoFinalDotNet8.Data;

namespace ProyectoFinalDotNet8.Components.Account
{
    // Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
    internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender emailSender = new NoOpEmailSender();

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            emailSender.SendEmailAsync(email, "Confirma tu correo electr�nico", $"Por favor, confirme su cuenta de la siguiente manera: <a href='{confirmationLink}'>Haciendo clic aqu�</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            emailSender.SendEmailAsync(email, "Restablece tu contrase�a", $"Por favor, restablezca su contrase�a de la siguiente manera: <a href='{resetLink}'>Haciendo clic aqu�</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            emailSender.SendEmailAsync(email, "Restablece tu contrase�a", $"Por favor, restablezca su contrase�a utilizando el siguiente c�digo: {resetCode}");
    }
}
