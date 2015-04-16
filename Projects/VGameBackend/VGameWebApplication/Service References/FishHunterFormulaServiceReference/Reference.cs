﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.18444
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace VGameWebApplication.FishHunterFormulaServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FormulaState", Namespace="http://schemas.datacontract.org/2004/07/VGame.Project.FishHunter.WCF")]
    [System.SerializableAttribute()]
    public partial struct FormulaState : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int CoreFpsField;
        
        private int PeerCountField;
        
        private int PeerFpsField;
        
        private int ReadBytesPerSecondField;
        
        private long TotalReadBytesField;
        
        private long TotalWriteBytesField;
        
        private int WattToReadField;
        
        private int WattToWriteField;
        
        private int WriteBytesPerSecondField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int CoreFps {
            get {
                return this.CoreFpsField;
            }
            set {
                if ((this.CoreFpsField.Equals(value) != true)) {
                    this.CoreFpsField = value;
                    this.RaisePropertyChanged("CoreFps");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PeerCount {
            get {
                return this.PeerCountField;
            }
            set {
                if ((this.PeerCountField.Equals(value) != true)) {
                    this.PeerCountField = value;
                    this.RaisePropertyChanged("PeerCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int PeerFps {
            get {
                return this.PeerFpsField;
            }
            set {
                if ((this.PeerFpsField.Equals(value) != true)) {
                    this.PeerFpsField = value;
                    this.RaisePropertyChanged("PeerFps");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int ReadBytesPerSecond {
            get {
                return this.ReadBytesPerSecondField;
            }
            set {
                if ((this.ReadBytesPerSecondField.Equals(value) != true)) {
                    this.ReadBytesPerSecondField = value;
                    this.RaisePropertyChanged("ReadBytesPerSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long TotalReadBytes {
            get {
                return this.TotalReadBytesField;
            }
            set {
                if ((this.TotalReadBytesField.Equals(value) != true)) {
                    this.TotalReadBytesField = value;
                    this.RaisePropertyChanged("TotalReadBytes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long TotalWriteBytes {
            get {
                return this.TotalWriteBytesField;
            }
            set {
                if ((this.TotalWriteBytesField.Equals(value) != true)) {
                    this.TotalWriteBytesField = value;
                    this.RaisePropertyChanged("TotalWriteBytes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int WattToRead {
            get {
                return this.WattToReadField;
            }
            set {
                if ((this.WattToReadField.Equals(value) != true)) {
                    this.WattToReadField = value;
                    this.RaisePropertyChanged("WattToRead");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int WattToWrite {
            get {
                return this.WattToWriteField;
            }
            set {
                if ((this.WattToWriteField.Equals(value) != true)) {
                    this.WattToWriteField = value;
                    this.RaisePropertyChanged("WattToWrite");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int WriteBytesPerSecond {
            get {
                return this.WriteBytesPerSecondField;
            }
            set {
                if ((this.WriteBytesPerSecondField.Equals(value) != true)) {
                    this.WriteBytesPerSecondField = value;
                    this.RaisePropertyChanged("WriteBytesPerSecond");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FishHunterFormulaServiceReference.IFormulaService")]
    public interface IFormulaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFormulaService/GetState", ReplyAction="http://tempuri.org/IFormulaService/GetStateResponse")]
        VGameWebApplication.FishHunterFormulaServiceReference.FormulaState GetState();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFormulaService/GetState", ReplyAction="http://tempuri.org/IFormulaService/GetStateResponse")]
        System.Threading.Tasks.Task<VGameWebApplication.FishHunterFormulaServiceReference.FormulaState> GetStateAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFormulaServiceChannel : VGameWebApplication.FishHunterFormulaServiceReference.IFormulaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FormulaServiceClient : System.ServiceModel.ClientBase<VGameWebApplication.FishHunterFormulaServiceReference.IFormulaService>, VGameWebApplication.FishHunterFormulaServiceReference.IFormulaService {
        
        public FormulaServiceClient() {
        }
        
        public FormulaServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FormulaServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormulaServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormulaServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public VGameWebApplication.FishHunterFormulaServiceReference.FormulaState GetState() {
            return base.Channel.GetState();
        }
        
        public System.Threading.Tasks.Task<VGameWebApplication.FishHunterFormulaServiceReference.FormulaState> GetStateAsync() {
            return base.Channel.GetStateAsync();
        }
    }
}