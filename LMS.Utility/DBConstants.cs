using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Utility
{
    public class DBConstants
    {
        /// <summary>The maximum string length for a Display Name column</summary>
        public const int MaxDisplayNameLength = 128;

        public const int MaxRollNoLength = 20;

        public const int MaxStatusLength = 20;

        public const int MaxLevelLength = 20;
        
        /// <summary>The maximum string length of an enumeration name (eg: enum.ToString().Length)</summary>
        public const int MaxEnumStringLength = 32;
        public const string MaxEnumStringLengthDbType = "nvarchar(32)";

        /// <summary>The maximum string length of a Codeset Code column</summary>
        public const int MaxCodeLength = 64;
        public const string MaxCodeLengthDbType = "nvarchar(64)";

        /// <summary>The maximum string length of an OID column</summary>
        public const int MaxOidLength = 64;

        /// <summary>The maximum string length of a Session Id/Token column</summary>
        public const int MaxSessionIdLength = 64;

        /// <summary>The maximum string length of an Ip Address column</summary>
        public const int MaxIpAddressLength = 32;

        /// <summary>The maximum string length of a URL column</summary>
        public const int MaxUrlLength = 1024;

        /// <summary>The maximum string length of a Phone Number column</summary>
        public const int MaxPhoneLength = 32;

        /// <summary>The maximum string length of email column</summary>
        public const int MaxEmailLength = 64;

        /// <summary>The maximum string length of a Address Line column</summary>
        public const int MaxAddressLineLength = 256;

        /// <summary>The maximum string length of City, State, Country</summary>
        public const int MaxAddressLocalityLength = 64;

        /// <summary>The maximum string length of Postal Code</summary>
        public const int MaxZipCodeLength = 8;

        /// <summary>The maximum string length of Dose string</summary>
        public const int MaxDoseLength = 10;

        public const int MaxYearLength = 4;

        public const int MaxResult = 6;

        /// <summary>The maximum string length of LotNumber</summary>
        public const int MaxLotNumberLength = 20;

        /// <summary>The maximum string length of ActionCode</summary>
        public const int MaxActionCodeLength = 1;

        /// <summary>The maximum string length of Description column</summary>
        public const int MaxDescriptionLength = 128;

        /// <summary>The maximum string length of notes column</summary>
        public const int MaxNotesLength = 2048;

        /// <summary>The maximum string length of Postal Code</summary>
        public const int MaxStripeCustomerKeyLength = 64;
    }
}

/*
 * 
 * 
 *
 // ----------------------------------------------
// Copyright 2024 PsychPlus, all rights reserved.
// ----------------------------------------------

namespace FTHealthCare.Common.Constants;

/// <summary>
/// Maximum constants used throughout the product.<br/><br/>
/// <b>WARNING: These are also used in database definitions. Investigate before thinking of changing ANYTHING in here!</b>
/// </summary>
public static class MaxConstants
{
    /// <summary>The maximum string length for a Display Name column</summary>
    public const int MaxDisplayNameLength = 128;
    
    /// <summary>The maximum string length of an enumeration name (eg: enum.ToString().Length)</summary>
    public const int MaxEnumStringLength = 32;
    public const string MaxEnumStringLengthDbType = "nvarchar(32)";

    /// <summary>The maximum string length of a Codeset Code column</summary>
    public const int MaxCodeLength = 64;
    public const string MaxCodeLengthDbType = "nvarchar(64)";

    /// <summary>The maximum string length of an OID column</summary>
    public const int MaxOidLength = 64;

    /// <summary>The maximum string length of a Session Id/Token column</summary>
    public const int MaxSessionIdLength = 64;
    
    /// <summary>The maximum string length of an Ip Address column</summary>
    public const int MaxIpAddressLength = 32;
    
    /// <summary>The maximum string length of a URL column</summary>
    public const int MaxUrlLength = 1024;

    /// <summary>The maximum string length of a Phone Number column</summary>
    public const int MaxPhoneLength = 32;

    /// <summary>The maximum string length of email column</summary>
    public const int MaxEmailLength = 64;

    /// <summary>The maximum string length of a Address Line column</summary>
    public const int MaxAddressLineLength = 256;

    /// <summary>The maximum string length of City, State, Country</summary>
    public const int MaxAddressLocalityLength = 64;

    /// <summary>The maximum string length of Postal Code</summary>
    public const int MaxZipCodeLength = 8;

    /// <summary>The maximum string length of Dose string</summary>
    public const int MaxDoseLength = 10;

    /// <summary>The maximum string length of LotNumber</summary>
    public const int MaxLotNumberLength = 20;

    /// <summary>The maximum string length of ActionCode</summary>
    public const int MaxActionCodeLength = 1;
    
    /// <summary>The maximum string length of Description column</summary>
    public const int MaxDescriptionLength = 128;
    
    /// <summary>The maximum string length of notes column</summary>
    public const int MaxNotesLength = 2048;

    /// <summary>The maximum string length of Postal Code</summary>
    public const int MaxStripeCustomerKeyLength = 64;
}

 */