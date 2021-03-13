using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        //private _subscription para podermos adicionar e ter outros metodos referente a lista de subscriptions nessa classe
        private IList<Subscription> _subscription;

        //Single responsibility principle
        //criar construtores para saber oque é requerido ao instanciar uma classe
        //com apenas 1 construtor, criamos um unico ponto de entrada
        public Student(string firstName, string lastName, string document, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            _subscription = new List<Subscription>();
        }

        //Single responsibility principle
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        //IReadOnlyCollection para nao ser possivel adicionar subscription por fora da classe
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscription.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            _subscription.Add(subscription);
        }
    }
}