#!/usr/bin/env bash

# Updating Info.plist

PLIST=$BUILD_REPOSITORY_LOCALPATH/MyApp/iOS/Info.plist

/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString 5.0.${APPCENTER_BUILD_ID}" $PLIST

# Print out file for reference
cat $PLIST

echo "Updated info.plist!"