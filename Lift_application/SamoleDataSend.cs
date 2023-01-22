using Lift_application.Models;

namespace Lift_application
{
    public class SamoleDataSend
    {
        public static void Initialize(EmailForSendContext context)
        {
            if (!context.EmailForSend.Any())
            {
                context.EmailForSend.AddRange
                    (
                        new EmailForSend
                        {
                            
                            Email = "simen204@mail.ru"
                        }

                    ) ;

                context.SaveChanges();
            }

        }
    }
}
