using System;
using Flunt.Notifications;

namespace PaymentContext.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        //uso guid ao inves de int id, pois assim é possível gerar ele do lado no .net, ao inves de ser gerado um int no pelo banco
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}