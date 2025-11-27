using ProyectoFinalDraft.Interfaces;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Services
    {
    public class ConsolePromotionObserver : IObserverPromotion
        {
        public void Update(Promocion promocion)
            {
            Console.WriteLine($"Nueva promoción: {promocion.Titulo} - {promocion.Descripcion}");
            }
        }
    }
