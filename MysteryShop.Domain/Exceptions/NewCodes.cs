using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Domain.Exceptions
{
    public static class NewCodes
    {
        public static string ShortComment => "Comment is too short! (min 20 characters)";
        public static string LongComment => "Comment is to long! (max 200 characters)";
        public static string ShortDescryption => "Descryption is too short! (min 5 characters)";
        public static string LongDescryption => "Descryption is too long! (max 2500 characters)";
        public static string NullLogin => "Empty Login!";
        public static string WrongCharacterLogin => "Niedozwolone znaki w Loginie!";
        public static string NullPassword => "Password is empty or contains white characters!";
        public static string WrongSalt => "Wrong salt!";
        public static string ShortPassword => "Password is too short! (min 8 characters)";
        public static string LongPassword => "Password is too long! (max 32 characters)";
        public static string WrongStatus => "Wrong Status!";
        public static string WrongCredentials => "Wrong credentials!";
        public static string UserNotFound => "User not found!";
        public static string CategoryNotFound => "Nie znaleziono kategori!";
        public static string WrongUserName => "Wrong user name";
        public static string LongLogin => "Login is too long! (max 15 characters)";
        public static string ShortLogin => "Login is to short! (min 4 characters)";
        public static string LoginTaken => "Użytkownik o takim loginie już istnieje";
        public static string LongUserName => "Nazwa użytkownika zbyt długa! (max 15 znaków)";
        public static string ShortUserName => "Nazwa użytkownika zbyt krótka! (min 4 zanki)";
        public static string UserNameTaken => "Nazwa użytkownika zajęta!";
        public static string ShorServiceName => "Zbyt krótka nazwa usługi! (min 4 znaki)";
        public static string LongServiceName => "Zbyt dluga nazwa usługi! (max 20 znaków)";
        public static string PermissionDenied => "Nie masz uprawnień do tego zasobu.";
        public static string ActiveName => "Masz już produkt o takiej nazwie!";
        public static string UserVoted => "Użytkownik już wystawił ocenę dla tego produktu.";
        public static string WrongRating => "Podana wartość oceny jest nieprawidłowa.";
        public static string LongSurname => "Surname is too long! (max 30 characters)";
        public static string ShortSurname => "Surname is too short! (min 3 characters)";
        public static string LongName => "Name is to long! (max 30 characters)";
        public static string ShortName => "Name is to short! (min 3 characters)";
        public static string WrongSurname => "Surname contains forbiden characters!";
        public static string WrongName => "Name contains forbiden characters!";
        public static string ShortTitle => "Title is too short! (min 3 characters)";
        public static string LongTitle => "Title is too long! (max 50 characters)";
        public static string WrongEmail => "Wrong email format!";
        public static string NotUniqueEmail => "Mail is not unique!";
        public static string SameEmail => "New email is same as old!";
        public static string EmptyDescryption => "Descryption is empty!";
        public static string EmptyTitle => "Title is empty!";
        public static string ProductNotFound => "Product not found!";
    }
}
