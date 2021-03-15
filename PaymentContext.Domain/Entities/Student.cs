using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    //Single responsibility principle(classe trata apenas de coisas relacionadas ao student)
    public class Student : Entity
    {
        //private _subscription para podermos adicionar e ter outros metodos referente a lista de subscriptions nessa classe
        private IList<Subscription> _subscriptions;

        //criar construtores para saber oque é requerido ao instanciar uma classe
        //com apenas 1 construtor, criamos um unico ponto de entrada
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            //agrupar erros
            AddNotifications(name, document, email);
        }

        //evitar usar muitos tipos primitivos, pois usando um value object, podemos criar regras, validacoes etc 
        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        //IReadOnlyCollection para nao ser possivel adicionar subscription por fora da classe
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos")
            );

            if (Valid)
                _subscriptions.Add(subscription);
            // Alternativa
            // if (hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");
        }
    }
}