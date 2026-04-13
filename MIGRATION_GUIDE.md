# Migration Guide: Upgrading to BigBlueButton API v3 Support

This guide helps you upgrade from the existing `BigBlueButtonApiV2_2` class to the new `BigBlueButtonApiV3` class for BigBlueButton 3.x server compatibility.

## Overview of Changes

| Area | V2_2 (existing) | V3 (new) |
|---|---|---|
| Checksum algorithm | SHA-1 | SHA-256 (default), configurable |
| Join authentication | Password-based | Role-based (`MODERATOR` / `VIEWER`) |
| End meeting auth | Requires moderator password | Checksum-only (password optional) |
| Create parameters | BBB 2.2 set | All BBB 3.x parameters |
| New endpoints | — | `insertDocument`, `sendChatMessage` |
| Deprecated APIs | Available | Still available in V2_2 base |

## Step 1: Switch to `BigBlueButtonApiV3`

**Before (V2_2):**
```csharp
IBigBlueButtonApi api = new BigBlueButtonApiV2_2("https://bbb.example.com/bigbluebutton/api/", "your-secret");
```

**After (V3):**
```csharp
IBigBlueButtonApiV3 api = new BigBlueButtonApiV3("https://bbb.example.com/bigbluebutton/api/", "your-secret");
```

The `BigBlueButtonApiV3` class extends `BigBlueButtonApiV2_2`, so all V2 methods remain available. The only difference is that the default checksum algorithm is now SHA-256.

### Custom hash algorithm

If your BBB server is configured to only accept a specific algorithm:

```csharp
// Use SHA-512 instead of the default SHA-256
var api = new BigBlueButtonApiV3("https://bbb.example.com/bigbluebutton/api/", "your-secret", false, ChecksumHashAlgorithm.SHA512);
```

Available values: `ChecksumHashAlgorithm.SHA1`, `SHA256`, `SHA384`, `SHA512`.

### Staying on V2_2 with a newer hash algorithm

If you are not ready to migrate to V3 but need SHA-256 checksums:

```csharp
var api = new BigBlueButtonApiV2_2("https://bbb.example.com/bigbluebutton/api/", "your-secret", false, ChecksumHashAlgorithm.SHA256);
```

## Step 2: Update Join Calls to Use Roles

BBB 3.x introduces role-based joining. Passwords are optional and may not even be set on meetings.

**Before (password-based):**
```csharp
var result = await api.JoinAsync("John Doe", "meeting-123", "attendee-password");
```

**After (role-based):**
```csharp
// Use JoinAsyncV3 with the role parameter
var result = await api.JoinAsyncV3("John Doe", "meeting-123", role: "VIEWER");

// For moderators:
var result = await api.JoinAsyncV3("Jane Admin", "meeting-123", role: "MODERATOR");

// Or generate a URI without executing the request:
Uri joinUrl = api.JoinUriV3("John Doe", "meeting-123", role: "VIEWER");
```

**Backward-compatible (password still works):**
```csharp
// Passwords still work for backward compatibility with BBB 2.x
var result = await api.JoinAsyncV3("John Doe", "meeting-123", password: "attendee-password");
```

### New join parameters

`JoinAsyncV3` and `JoinUriV3` support these additional parameters:

| Parameter | Type | Description |
|---|---|---|
| `role` | `string` | `"MODERATOR"` or `"VIEWER"` — preferred over password |
| `enforceLayout` | `string` | Force a specific layout for this user |
| `excludeFromDashboard` | `string` | `"true"` to exclude from learning dashboard |
| `errorRedirectUrl` | `string` | URL to redirect on error |

> **Note:** The deprecated `configToken` and `joinViaHtml5` parameters have been removed from the V3 join methods. The HTML5 client is the only client in BBB 3.x.

## Step 3: Update End Meeting Calls

**Before (password required):**
```csharp
var result = await api.EndAsync("meeting-123", "moderator-password");
```

**After (password optional):**
```csharp
// In BBB 3.x, authentication is via the checksum (shared secret)
var result = await api.EndAsyncV3("meeting-123");

// Password can still be provided for backward compatibility
var result = await api.EndAsyncV3("meeting-123", "moderator-password");
```

## Step 4: Use New Create Parameters

`CreateAsyncV3` supports all BBB 3.x create parameters. Passwords are optional in BBB 3.x.

**Before:**
```csharp
var result = await api.CreateAsync("meeting-123", "My Meeting", "att-pw", "mod-pw", "Welcome!");
```

**After:**
```csharp
var result = await api.CreateAsyncV3(
    "meeting-123",
    name: "My Meeting",
    welcome: "Welcome!",
    // New BBB 3.x parameters:
    endWhenNoModerator: true,
    endWhenNoModeratorDelayInMinutes: 5,
    meetingCameraCap: 10,
    userCameraCap: 2,
    disabledFeatures: "chat,captions",
    lockSettingsHideViewersCursor: true,
    lockSettingsHideViewersAnnotation: true
);
```

### New create parameters

| Parameter | Type | Description |
|---|---|---|
| `endWhenNoModerator` | `bool?` | End when no moderator present |
| `endWhenNoModeratorDelayInMinutes` | `int?` | Delay before ending |
| `meetingKeepEvents` | `bool?` | Save events even if not recorded |
| `allowModsToEjectCameras` | `bool?` | Moderators can eject webcams |
| `meetingCameraCap` | `int?` | Max webcams for meeting |
| `userCameraCap` | `int?` | Max webcams per user |
| `meetingExpireIfNoUserJoinedInMinutes` | `int?` | Expire if nobody joins |
| `meetingEndedURL` | `string` | Callback URL when meeting ends |
| `disabledFeatures` | `string` | Comma-separated disabled features |
| `disabledFeaturesExclude` | `string` | Re-enable features from disabled list |
| `notifyRecordingIsOn` | `bool?` | Notify users of recording |
| `presentationUploadExternalUrl` | `string` | External upload URL |
| `presentationUploadExternalDescription` | `string` | External upload description |
| `learningDashboardCleanupDelayInMinutes` | `int?` | Dashboard cleanup delay |
| `recordFullDurationMedia` | `bool?` | Record full duration |
| `lockSettingsHideUserList` | `bool?` | Hide user list from viewers |
| `lockSettingsHideViewersCursor` | `bool?` | Hide viewer cursors |
| `lockSettingsHideViewersAnnotation` | `bool?` | Hide viewer annotations |
| `groups` | `string` | User groups (JSON) |
| `breakoutRoomsRecord` | `bool?` | Record breakout rooms |
| `breakoutRoomsPrivateChatEnabled` | `bool?` | Private chat in breakouts |
| `allowPromoteGuestToModerator` | `bool?` | Promote guests to moderator |
| `preUploadedPresentationOverrideDefault` | `bool?` | Override default presentation |

## Step 5: Use New BBB 3.x Endpoints

### Insert Document

Insert presentation documents into a running meeting:

```csharp
var slides = new CreateRequest
{
    Modules = new Modules
    {
        Module = new Module
        {
            Name = "presentation",
            Documents = new List<Document>
            {
                new Document { Url = "https://example.com/slides.pdf", Filename = "slides.pdf" }
            }
        }
    }
};

var result = await api.InsertDocumentAsync("meeting-123", slides);
```

### Send Chat Message

Send a system chat message to a running meeting:

```csharp
var result = await api.SendChatMessageAsync("meeting-123", "Hello from the API!");

// With a custom sender name:
var result = await api.SendChatMessageAsync("meeting-123", "Meeting starts in 5 minutes", userName: "Reminder Bot");
```

## Step 6: Handle Deprecated APIs

The following APIs have been removed from BigBlueButton 3.x and are marked as `[Obsolete]`:

- `GetDefaultConfigXmlAsync()` — The Flash client is no longer supported
- `SetDefaultConfigXmlAsync()` — The Flash client is no longer supported

These methods still exist in the `BigBlueButtonApiV2_2` base class for backward compatibility with BBB 2.x servers, but will produce compiler warnings.

## Bug Fixes

The following bugs from the V2_2 implementation have been fixed:

1. **`SetDefaultConfigXmlAsync`**: The endpoint URL was incorrectly set to `getDefaultConfigXML` instead of `setConfigXML`, and the `meetingID` parameter was being set to `configXML` instead of the actual meeting ID.

## Quick Reference: Interface Hierarchy

```
IBigBlueButtonApi          ← V2.x interface (unchanged)
    └── IBigBlueButtonApiV3    ← V3 interface (adds V3 methods)

BigBlueButtonApiBase       ← Checksum, query parameter helpers
    └── BigBlueButtonApiV2_2   ← V2.x implementation (SHA-1 default)
            └── BigBlueButtonApiV3 ← V3 implementation (SHA-256 default)
```

Use `IBigBlueButtonApiV3` when you need V3 features. Use `IBigBlueButtonApi` if you only need V2 compatibility.
