﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.1433
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AvrLib {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("hidboot.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Checksum error. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string HEXLOADER_CHECKSUM_ERROR {
            get {
                return ResourceManager.GetString("HEXLOADER_CHECKSUM_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Invalid Intel-HEX data. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string HEXLOADER_INVALID_INTEL_HEX {
            get {
                return ResourceManager.GetString("HEXLOADER_INVALID_INTEL_HEX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Invalid record was found. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string HEXLOADER_INVALID_RECORD {
            get {
                return ResourceManager.GetString("HEXLOADER_INVALID_RECORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   This file format isn&apos;t supported. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string HEXLOADER_NOT_SUPPORTED {
            get {
                return ResourceManager.GetString("HEXLOADER_NOT_SUPPORTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Record type {0} isn&apos;t supported. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string HEXLOADER_RECORD_NOT_SUPPORTED {
            get {
                return ResourceManager.GetString("HEXLOADER_RECORD_NOT_SUPPORTED", resourceCulture);
            }
        }
    }
}
