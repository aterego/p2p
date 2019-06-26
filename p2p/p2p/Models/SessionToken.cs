using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace p2p.Models
{
    public class SessionToken : INotifyPropertyChanged
    {
        private string _username;
        private string _accessToken;
        private string _refreshToken;
        private long _expiration;
        private string _role;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        /// <summary>
        /// Gets or sets the accessToken.
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AccessToken"));
            }
        }

        /// <summary>
        /// Gets or sets the accessToken.
        /// </summary>
        public string RefreshToken
        {
            get { return _refreshToken; }
            set
            {
                _refreshToken = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RefreshToken"));
            }
        }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        public long Expiration
        {
            get { return _expiration; }
            set
            {
                _expiration = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Expiration"));
            }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Role"));
            }
        }


    }
}
