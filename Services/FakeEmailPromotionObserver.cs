using ProyectoFinalDraft.Interfaces;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Services
    {
    public class FakeEmailPromotionObserver : IObserverPromotion
        {
            public void OnPromotionCreated(string promotionTitle, string promotionDescription)
            {
                
            }

        public void Update(Promocion promocion)
            {
            // Simula el envío de un correo electrónico notificando sobre la nueva promoción
            Console.WriteLine("Enviando correo electrónico de promoción...");
            Console.WriteLine($"Título: {promocion.Titulo}");
            Console.WriteLine($"Descripción: {promocion.Descripcion}");
            Console.WriteLine("Correo electrónico enviado a los usuarios suscritos a promociones.");
            }
        }
    }
