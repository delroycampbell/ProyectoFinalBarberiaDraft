using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Interfaces
    {
    public interface ISubjectPromotion
        {
            void Attach(IObserverPromotion observer);
            void Detach(IObserverPromotion observer);
            void Notify(Promocion promocion);
        }
    }
