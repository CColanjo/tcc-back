namespace schedule_appointment_domain.Model.Request
{
    public class OAuthResetPasswordConfirmation
    {
        /// <summary>
        /// Email
        /// </summary>        
        public string Username { get; set; } = default!;

        /// <summary>
        /// Password
        /// </summary>        
        public string OldPassword { get; set; } = default!;

        /// <summary>
        /// New Password
        /// </summary>        
        public string NewPassword { get; set; } = default!;

        /// <summary>
        /// New Password Confirmation
        /// </summary>        
        public string NewPasswordConfirmation { get; set; } = default!;
    }
}
