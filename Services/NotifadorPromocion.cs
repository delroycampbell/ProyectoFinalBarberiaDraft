using ProyectoFinalDraft.Interfaces;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Services
    {
    public class NotifadorPromocion : ISubjectPromotion
        {
        private readonly List<IObserverPromotion> observadores = new List<IObserverPromotion>();
        public void Attach(IObserverPromotion observer)
            {
            observadores.Add(observer);
            }

        public void Detach(IObserverPromotion observer)
            {
            observadores.Remove(observer);
            }

        public void Notify(Promocion promocion)
            {
            foreach (var observer in observadores)
                {
                observer.Update(promocion);
                }
            }
        }
    }

