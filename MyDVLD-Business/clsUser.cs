using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsUser
    {
        private enum _enMode { Update, AddNew }
        private _enMode _Mode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }     
        public int Permission { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo;
        public clsUser()
        {
            _Mode = _enMode.AddNew;
        }
        public clsUser(int UserID , int PersonID, string userName, string password,//check UserID and replace to PersonID
             int permission, bool isActive)//Remove nationalNo
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            UserName = userName;
            Password = password;          
            Permission = permission;
            IsActive = isActive;
            PersonInfo = clsPerson.FindByPersonID(PersonID);
            _Mode = _enMode.Update;
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int PersonID = -1, Permission = -1, UserID = -1; bool IsActive = false;
            
            if (clsUserData.FindUserByUserIDAndPassword(ref UserID, ref PersonID, UserName,  Password, ref IsActive, ref Permission))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Permission, IsActive);
            }
            return null;

        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID=-1,Permission=-1;bool IsActive=false;
            string UserName = "", Password = "";
            if (clsUserData.GetInfoUserByUserID(UserID,ref PersonID,ref UserName,ref Password,ref IsActive,ref Permission))
            {
                return new clsUser(UserID, PersonID, UserName,Password, Permission, IsActive);
            }
            return null;

        }
        public static clsUser FindByUserName(string UserName)
        {
            int PersonID = -1, Permission = -1, UserID = -1; bool IsActive = false;
            string Password = "";
            if (clsUserData.GetInfoUserByUserName(ref UserID, ref PersonID,  UserName, ref Password, ref IsActive, ref Permission))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Permission, IsActive);
            }
            return null;

        }
        private bool _AddNew()
        {
            
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, Password, IsActive, Permission);
            return (UserID > 0);
        }
        private bool _Update()
        {
            return clsUserData.UpdateUser(UserID,PersonID,UserName,Password, IsActive,Permission);
        }
        public static DataTable ListUsers()
        {
            return clsUserData.GetListUsers();
        }
        public bool Save()
        {
            switch (_Mode) 
            {
                case _enMode.AddNew:
                    if(_AddNew())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
                case _enMode.Update:
                    return _Update();
            }
            return false;
        }

        public static bool IsExistByPersonID(int PersonID)
        {
            return clsUserData.IsExistByPersonID(PersonID);
        }
        public static bool IsExistByUserName(string UserName)
        {
            return clsUserData.IsExistByUserName(UserName);
        }
        public static bool DeleteUser(int UserID)
        {
            return (clsUserData.DeleteUser(UserID));
        }
    }
}
