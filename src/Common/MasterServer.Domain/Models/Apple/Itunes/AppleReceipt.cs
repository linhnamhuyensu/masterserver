﻿namespace Iap.Verify.Models
{
    /// <summary>
    /// See, https://developer.apple.com/documentation/appstorereceipts/responsebody/receipt?language=objc
    /// </summary>
    public class AppleReceipt
    {
        /// <summary>
        /// See app_item_id.
        /// </summary>
        public long AdamId { get; set; }

        /// <summary>
        /// Generated by App Store Connect and used by the App Store to uniquely identify the app purchased. Apps are assigned this identifier only in production. Treat this value as a 64-bit long integer.
        /// </summary>
        public long AppItemId { get; set; }

        /// <summary>
        /// The app’s version number. The app's version number corresponds to the value of <em>CFBundleVersion</em> (in iOS) or <em>CFBundleShortVersionString</em> (in macOS) in the Info.plist. In production, this value is the current version of the app on the device based on the <em>receipt_creation_date_ms</em>. In the sandbox, the value is always "1.0".
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// The bundle identifier for the app to which the receipt belongs. You provide this string on App Store Connect. This corresponds to the value of <em>CFBundleIdentifier</em> in the Info.plist file of the app.
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>
        /// A unique identifier for the app download transaction.
        /// </summary>
        public long DownloadId { get; set; }

        /// <summary>
        /// The time the receipt expires for apps purchased through the Volume Purchase Program, in a date-time format similar to the ISO 8601.
        /// </summary>
        public string ExpirationDate { get; set; }

        /// <summary>
        /// The time the receipt expires for apps purchased through the Volume Purchase Program, in UNIX epoch time format, in milliseconds. If this key is not present for apps purchased through the Volume Purchase Program, the receipt does not expire. Use this time format for processing dates.
        /// </summary>
        public string ExpirationDateMs { get; set; }

        /// <summary>
        /// The time the receipt expires for apps purchased through the Volume Purchase Program, in the Pacific Time zone.
        /// </summary>
        public string ExpirationDatePst { get; set; }

        /// <summary>
        /// An array that contains the in-app purchase receipt fields for all in-app purchase transactions.
        /// </summary>
        public List<AppleInApp> InApp { get; set; }

        /// <summary>
        /// The version of the app that the user originally purchased. This value does not change, and corresponds to the value of <em>CFBundleVersion</em> (in iOS) or <em>CFBundleShortVersionString</em> (in macOS) in the Info.plist file of the original purchase. In the sandbox environment, the value is always "1.0".
        /// </summary>
        public string OriginalApplicationVersion { get; set; }

        /// <summary>
        /// The time of the original app purchase, in a date-time format similar to ISO 8601.
        /// </summary>
        public string OriginalPurchaseDate { get; set; }

        /// <summary>
        /// The time of the original app purchase, in UNIX epoch time format, in milliseconds. Use this time format for processing dates.
        /// </summary>
        public string OriginalPurchaseDateMs { get; set; }

        /// <summary>
        /// The time of the original app purchase, in the Pacific Time zone.
        /// </summary>
        public string OriginalPurchaseDatePst { get; set; }

        /// <summary>
        /// The time the user ordered the app available for pre-order, in a date-time format similar to ISO 8601.
        /// </summary>
        public string PreorderPurchaseDate { get; set; }

        /// <summary>
        /// The time the user ordered the app available for pre-order, in UNIX epoch time format, in milliseconds. This field is only present if the user pre-orders the app. Use this time format for processing dates.
        /// </summary>
        public string PreorderPurchaseDateMs { get; set; }

        /// <summary>
        /// The time the user ordered the app available for pre-order, in the Pacific Time zone.
        /// </summary>
        public string PreorderPurchaseDatePst { get; set; }

        /// <summary>
        /// The time the App Store generated the receipt, in a date-time format similar to ISO 8601.
        /// </summary>
        public string ReceiptCreationDate { get; set; }

        /// <summary>
        /// The time the App Store generated the receipt, in UNIX epoch time format, in milliseconds. Use this time format for processing dates. This value does not change.
        /// </summary>
        public string ReceiptCreationDateMs { get; set; }

        /// <summary>
        /// The time the App Store generated the receipt, in the Pacific Time zone.
        /// </summary>
        public string ReceiptCreationDatePst { get; set; }

        /// <summary>
        /// The type of receipt generated. The value corresponds to the environment in which the app or VPP purchase was made.
        ///
        /// Possible values,
        /// <em>Production</em>, <em>ProductionVPP</em>, <em>ProductionSandbox</em>, <em>ProductionVPPSandbox</em>
        /// </summary>
        public string ReceiptType { get; set; }

        /// <summary>
        /// The time the request to the <em>verifyReceipt</em> endpoint was processed and the response was generated, in a date-time format similar to ISO 8601.
        /// </summary>
        public string RequestDate { get; set; }

        /// <summary>
        /// The time the request to the <em>verifyReceipt</em> endpoint was processed and the response was generated, in UNIX epoch time format, in milliseconds. Use this time format for processing dates.
        /// </summary>
        public string RequestDateMs { get; set; }

        /// <summary>
        /// The time the request to the <em>verifyReceipt</em> endpoint was processed and the response was generated, in the Pacific Time zone.
        /// </summary>
        public string RequestDatePst { get; set; }

        /// <summary>
        /// An arbitrary number that identifies a revision of your app. In the sandbox, this key's value is “0”.
        /// </summary>
        public int VersionExternalIdentifier { get; set; }
    }
}