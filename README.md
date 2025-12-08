# Word Quran - Memorization Table Add-in

A Microsoft Word add-in that creates customizable memorization tables for Quran Juz. This tool helps students and memorizers track their Quran memorization progress with structured tables.

## Features

- **Select Juz**: Choose from all 30 Juz of the Quran
- **Create Tables**: Automatically generate memorization tracking tables
- **Customizable Options**:
  - Include/exclude page numbers
  - Include/exclude date column
  - Include/exclude notes column
- **Pre-filled Data**: Tables come with day numbers and section placeholders

## How to Use

### Prerequisites
- Microsoft Word (Desktop or Online)
- Node.js and npm installed

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/fareedsuri/wordquran.git
   cd wordquran
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev-server
   ```

### Using the Add-in

1. **Open Word Document**: Open a new or existing Word document

2. **Sideload the Add-in**:
   - For Word Desktop: Run `npm start` to sideload the add-in
   - For Word Online: Manually upload the manifest.xml file

3. **Start the Add-on**: 
   - Go to the HOME tab in Word
   - Click "Show Taskpane" button in the Word Quran group

4. **Select Juz**: 
   - In the taskpane, choose your desired Juz from the dropdown
   - Each Juz displays its Surah range for easy identification

5. **Configure Options**:
   - Check/uncheck options for page numbers, date column, and notes column

6. **Create Table**: 
   - Click the "Create Memorization Table" button
   - The table will be inserted at the end of your document

## Table Structure

The generated table includes:
- **Day**: Sequential day numbers (Day 1, Day 2, etc.)
- **Pages**: Estimated page range (optional)
- **Verses**: Section placeholders
- **Date**: For recording when you memorized (optional)
- **Completed**: Checkbox symbols (☐) to mark completion
- **Notes**: Space for additional notes (optional)

## Development

### Build for Production
```bash
npm run build
```

### Validate Manifest
```bash
npm run validate
```

## Project Structure

```
wordquran/
├── src/
│   ├── taskpane/
│   │   ├── taskpane.html
│   │   └── taskpane.js
│   ├── commands/
│   │   ├── commands.html
│   │   └── commands.js
│   └── data/
│       └── quran-data.js
├── assets/
│   └── icon-*.png
├── manifest.xml
├── package.json
├── webpack.config.js
└── README.md
```

## Juz Information

The add-in includes data for all 30 Juz of the Quran, including:
- Juz number and name
- Starting Surah and verse
- Ending Surah and verse
- Brief description

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT License - feel free to use this project for your memorization journey!

## Support

For issues or questions, please create an issue on GitHub.
