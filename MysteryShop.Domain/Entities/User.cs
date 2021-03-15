using MysteryShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MysteryShop.Domain.Entities
{
    public class User
    {
        private static readonly Regex regex_login = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        private static readonly Regex regex_mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");


        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public int Status { get; protected set; }


        protected User() { }

        public User(string email, string name, string surname, string login, string password, IEncrypter encrypter, int status = 1)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetName(name);
            SetSurname(surname);
            SetLogin(login);
            SetPassword(password, encrypter);
            SetStatus(status);
        }

        public void SetEmail(string email)
        {
            if (!regex_mail.IsMatch(email)) throw new Exception();
            if (email == Email) throw new Exception();
            Email = email;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception();
            if (name.Length > 30) throw new Exception();
            if (name.Length < 3) throw new Exception();
            Name = name;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname)) throw new Exception();
            if (surname.Length > 30) throw new Exception();
            if (surname.Length < 3) throw new Exception();
            Surname = surname;
        }

        public void SetLogin(string login)
        {
            if (login.Length < 3) throw new Exception();
            if (login.Length > 20) throw new Exception();
            if (String.IsNullOrEmpty(login)) throw new Exception();
            if (!regex_login.IsMatch(login)) throw new Exception();
            Login = login;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception();
            if (password.Length < 6) throw new Exception();
            if (password.Length >= 20) throw new Exception();
            if (password == Password) throw new Exception();

            string salt = encrypter.GetSalt(password);
            string hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
        }

        public void SetStatus(int status)
        {
            if (status == 1 | status == 0) Status = status;
            else throw new Exception();
        }
    }
}
