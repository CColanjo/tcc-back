

using schedule_appointment_domain.Security;
using System.Text;

namespace schedule_appointment_service.Security
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword()
        {

            // Define os caracteres permitidos na senha
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+={}[]\\|:;\"'<>,.?/";

            // Define os conjuntos de caracteres para cada tipo de caractere obrigatório
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()_-+={}[]\\|:;\"'<>,.?/";

            // Cria um objeto Random para gerar números aleatórios
            Random random = new Random();

            // Cria um objeto StringBuilder para construir a senha
            StringBuilder passwordBuilder = new StringBuilder();

            // Adiciona um caractere aleatório de cada tipo obrigatório à senha
            passwordBuilder.Append(upperCaseChars[random.Next(upperCaseChars.Length)]);
            passwordBuilder.Append(lowerCaseChars[random.Next(lowerCaseChars.Length)]);
            passwordBuilder.Append(numberChars[random.Next(numberChars.Length)]);
            passwordBuilder.Append(specialChars[random.Next(specialChars.Length)]);

            // Adiciona os caracteres restantes à senha
            for (int i = 4; i < 10; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                passwordBuilder.Append(allowedChars[randomIndex]);
            }

            // Embaralha a senha gerada para aumentar a segurança
            for (int i = 0; i < passwordBuilder.Length; i++)
            {
                int randomIndex = random.Next(passwordBuilder.Length);
                char tempChar = passwordBuilder[i];
                passwordBuilder[i] = passwordBuilder[randomIndex];
                passwordBuilder[randomIndex] = tempChar;
            }

            // Retorna a senha gerada
            return passwordBuilder.ToString();
        }
    }
}