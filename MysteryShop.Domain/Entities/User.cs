using MysteryShop.Domain.Exceptions;
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
            if (!regex_mail.IsMatch(email)) throw new NewException(NewCodes.WrongEmail);
            if (email == Email) throw new NewException(NewCodes.NotUniqueEmail);
            Email = email;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new NewException(NewCodes.WrongName);
            if (name.Length > 30) throw new NewException(NewCodes.LongName);
            if (name.Length < 3) throw new NewException(NewCodes.ShortName);
            Name = name;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname)) throw new NewException(NewCodes.WrongSurname);
            if (surname.Length > 30) throw new NewException(NewCodes.LongSurname);
            if (surname.Length < 3) throw new Exception(NewCodes.ShortSurname);
            Surname = surname;
        }

        public void SetLogin(string login)
        {
            if (login.Length < 4) throw new NewException(NewCodes.ShortLogin);
            if (login.Length > 15) throw new NewException(NewCodes.LongLogin);
            if (String.IsNullOrEmpty(login)) throw new NewException();
            if (!regex_login.IsMatch(login)) throw new NewException();
            Login = login;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new NewException(NewCodes.NullPassword);
            if (password.Length < 8) throw new NewException(NewCodes.ShortPassword);
            if (password.Length >= 32) throw new NewException(NewCodes.LongPassword);
            if (password == Password) throw new Exception();

            string salt = encrypter.GetSalt(password);
            string hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
        }

        public void SetStatus(int status)
        {
            if (status == 1 | status == 0) Status = status;
            else throw new NewException(NewCodes.WrongStatus);
        }
    }
}
