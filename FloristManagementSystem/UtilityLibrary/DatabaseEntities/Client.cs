using System;

namespace UtilityLibrary
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreditCard { get; set; }
        public DateTime InscriptionDate { get; set; }

        public Client(int id, string firstName, string lastName, string email, string phoneNumber, string address, string creditCard, DateTime inscriptionDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreditCard = creditCard;
            InscriptionDate = inscriptionDate;
        }
    }
}
