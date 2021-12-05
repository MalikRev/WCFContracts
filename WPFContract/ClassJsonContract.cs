using System;
using System.ComponentModel;

namespace WPFContract
{
    class ClassJsonContract : INotifyPropertyChanged
    {
        private DateTime _data_Contract;
        private string _namber_Contract;
        private DateTime _date_App_Con;
        private bool _testEnData;
        private DateTime days = DateTime.Now.AddDays(-60);


        public int Id { get; set; }


        public DateTime DataContract
        {
            get { return _data_Contract; }
            set
            {
                if (_data_Contract == value)
                {
                    return;
                }

                _data_Contract = value;
                OnPropertyChanged("DataContract");
            }
        }


        public string NamberContract
        {
            get { return _namber_Contract; }
            set
            {
                if (_namber_Contract == value)
                {
                    return;
                }

                _namber_Contract = value;
                OnPropertyChanged("NamberContract");
            }
        }


        public DateTime DateAppCon
        {
            get { return _date_App_Con; }
            set
            {
                if (_date_App_Con == value)
                {
                    return;
                }

                _date_App_Con = value;
                OnPropertyChanged("DateAppCon");
            }
        }


        public bool TestEnData
        {
            get
            {
                if (_date_App_Con > days)
                {
                    _testEnData = true;
                }
                else
                {
                    _testEnData = false;
                }

                return _testEnData;
            }
            set { OnPropertyChanged("TestEnData"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            ////Если никто не подписался на событие            
            //if (PropertyChanged != null)
            //{
            //    //sender - object вызывающий событие
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //}

            //Invoke открывает скобочки
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
