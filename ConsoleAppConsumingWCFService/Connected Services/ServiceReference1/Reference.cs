﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleAppConsumingWCFService.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/MyWcfService")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Person", Namespace="http://schemas.datacontract.org/2004/07/MyWcfService.Models")]
    [System.SerializableAttribute()]
    public partial class Person : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Age {
            get {
                return this.AgeField;
            }
            set {
                if ((this.AgeField.Equals(value) != true)) {
                    this.AgeField = value;
                    this.RaisePropertyChanged("Age");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        ConsoleAppConsumingWCFService.ServiceReference1.CompositeType GetDataUsingDataContract(ConsoleAppConsumingWCFService.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(ConsoleAppConsumingWCFService.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertPerson", ReplyAction="http://tempuri.org/IService1/InsertPersonResponse")]
        int InsertPerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertPerson", ReplyAction="http://tempuri.org/IService1/InsertPersonResponse")]
        System.Threading.Tasks.Task<int> InsertPersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdatePerson", ReplyAction="http://tempuri.org/IService1/UpdatePersonResponse")]
        int UpdatePerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdatePerson", ReplyAction="http://tempuri.org/IService1/UpdatePersonResponse")]
        System.Threading.Tasks.Task<int> UpdatePersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeltePerson", ReplyAction="http://tempuri.org/IService1/DeltePersonResponse")]
        int DeltePerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeltePerson", ReplyAction="http://tempuri.org/IService1/DeltePersonResponse")]
        System.Threading.Tasks.Task<int> DeltePersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPerson", ReplyAction="http://tempuri.org/IService1/GetPersonResponse")]
        ConsoleAppConsumingWCFService.ServiceReference1.Person GetPerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPerson", ReplyAction="http://tempuri.org/IService1/GetPersonResponse")]
        System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person> GetPersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPersons", ReplyAction="http://tempuri.org/IService1/GetPersonsResponse")]
        ConsoleAppConsumingWCFService.ServiceReference1.Person[] GetPersons();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPersons", ReplyAction="http://tempuri.org/IService1/GetPersonsResponse")]
        System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person[]> GetPersonsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllPersons", ReplyAction="http://tempuri.org/IService1/GetAllPersonsResponse")]
        ConsoleAppConsumingWCFService.ServiceReference1.Person[] GetAllPersons();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllPersons", ReplyAction="http://tempuri.org/IService1/GetAllPersonsResponse")]
        System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person[]> GetAllPersonsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : ConsoleAppConsumingWCFService.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<ConsoleAppConsumingWCFService.ServiceReference1.IService1>, ConsoleAppConsumingWCFService.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public ConsoleAppConsumingWCFService.ServiceReference1.CompositeType GetDataUsingDataContract(ConsoleAppConsumingWCFService.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(ConsoleAppConsumingWCFService.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public int InsertPerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.InsertPerson(person);
        }
        
        public System.Threading.Tasks.Task<int> InsertPersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.InsertPersonAsync(person);
        }
        
        public int UpdatePerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.UpdatePerson(person);
        }
        
        public System.Threading.Tasks.Task<int> UpdatePersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.UpdatePersonAsync(person);
        }
        
        public int DeltePerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.DeltePerson(person);
        }
        
        public System.Threading.Tasks.Task<int> DeltePersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.DeltePersonAsync(person);
        }
        
        public ConsoleAppConsumingWCFService.ServiceReference1.Person GetPerson(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.GetPerson(person);
        }
        
        public System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person> GetPersonAsync(ConsoleAppConsumingWCFService.ServiceReference1.Person person) {
            return base.Channel.GetPersonAsync(person);
        }
        
        public ConsoleAppConsumingWCFService.ServiceReference1.Person[] GetPersons() {
            return base.Channel.GetPersons();
        }
        
        public System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person[]> GetPersonsAsync() {
            return base.Channel.GetPersonsAsync();
        }
        
        public ConsoleAppConsumingWCFService.ServiceReference1.Person[] GetAllPersons() {
            return base.Channel.GetAllPersons();
        }
        
        public System.Threading.Tasks.Task<ConsoleAppConsumingWCFService.ServiceReference1.Person[]> GetAllPersonsAsync() {
            return base.Channel.GetAllPersonsAsync();
        }
    }
}
