using AssetTracking2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;


string input = "";


AssetTracking.Load(); // Load from database and create objects.

while (true)
{
    AssetTracking.DisplayMenu();

    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            // show list of assets
            AssetTracking.ShowList();
            break;
        case "2":
            AssetTracking.ShowList(true); // sorted by office
            break;
        case "3":
            AssetTracking.AddAsset();
            break;
        case "4":
            AssetTracking.Remove(); // delete asset
            break;
        default:
            break;
    }

    if (input.ToLower() == "q")
    {
        break;
    }
}
