# Asset Tracker

## Description

This is a .NET 9.0 console application that can be used to track assets for a company.

## Features
- Add new assets via the console

- Automatically convert price from USD to the local currency based on the office location

- Exchange rates fetched live from an API

- Supported office locations:
    - Boston (USD)
    - London (GBP)
    - Tokyo (JPY)
    - Berlin (EUR)
    - Toronto (CAD)
    - Malmö (SEK)

- Lists and sorts assets by 1) office location 2) purchase date

- The assets are color coded based on their expiration date:
    - Red with a cross symbol: Expired
    - Red: Expiring within 3 months
    - Yellow: Expiring within 6 months
- Sample data is included for demonstration purposes

## Usage
- When running, the program will show the full list of assets (sample data)
- The program will prompt you to add a new asset (a) or quit (q)
- If you choose to add a new asset, you will be prompted to enter:
    - Type (e.g. Laptop, Smartphone etc.)
    - Brand
    - Model
    - Office location
    - Price in USD
    - Purchase date

- The program automatically:
    - Maps the office location to the correct currency
    - Converts the price from USD to the local currency using live exchange rates
    - Calculates the expiration date (3 years from purchase date)

- After adding an asset, the program will display the updated list of assets, sorted by office location and purchase date

## Technologies used
- Net 9.0
- C#
- REST API