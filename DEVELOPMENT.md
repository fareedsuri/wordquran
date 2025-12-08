# Development Guide - Word Quran Add-in

## Overview
This document provides technical details for developers who want to contribute to or modify the Word Quran add-in.

## Architecture

### Technology Stack
- **Frontend**: HTML, CSS, JavaScript (ES6+)
- **Office API**: Office.js (Word JavaScript API)
- **Build Tools**: Webpack 5, Babel
- **Package Manager**: npm

### Project Structure
```
wordquran/
├── src/                    # Source files
│   ├── taskpane/          # Main UI
│   │   ├── taskpane.html # UI layout
│   │   └── taskpane.js   # UI logic
│   ├── commands/          # Ribbon commands
│   │   ├── commands.html
│   │   └── commands.js
│   └── data/              # Data layer
│       └── quran-data.js  # Juz information
├── assets/                # Static assets
│   └── icon-*.png        # Add-in icons
├── dist/                  # Build output (gitignored)
├── manifest.xml          # Add-in manifest
├── webpack.config.js     # Build configuration
├── package.json          # Dependencies
└── .babelrc             # Babel configuration
```

## Development Setup

### Prerequisites
1. Node.js (v14 or later)
2. npm (v6 or later)
3. Microsoft Word (for testing)
4. Git

### Initial Setup
```bash
# Clone the repository
git clone https://github.com/fareedsuri/wordquran.git
cd wordquran

# Install dependencies
npm install

# Build the project
npm run build
```

### Development Server
```bash
# Start the webpack dev server with HTTPS
npm run dev-server
```

The dev server will:
- Run on https://localhost:3000
- Enable hot module replacement
- Serve the add-in files
- Auto-compile on changes

### Building for Production
```bash
# Production build (minified)
npm run build
```

Output will be in the `dist/` directory.

## Code Organization

### Data Layer (`src/data/quran-data.js`)

#### Juz Data Structure
```javascript
{
  number: 1,
  name: "Juz 1",
  startSurah: 1,
  startSurahName: "Al-Fatihah",
  startVerse: 1,
  endSurah: 2,
  endSurahName: "Al-Baqarah",
  endVerse: 141,
  description: "..."
}
```

#### Available Functions
- `getJuz(juzNumber)`: Get a specific Juz by number
- `getAllJuzNames()`: Get all Juz for dropdown display

### UI Layer (`src/taskpane/`)

#### taskpane.html
- Responsive design using vanilla CSS
- Mobile-friendly layout
- Accessibility considerations
- No external CSS frameworks

#### taskpane.js
Main functions:
- `Office.onReady()`: Initialization
- `populateJuzDropdown()`: Populate Juz selector
- `handleJuzSelection()`: Enable/disable create button
- `createTable()`: Main table creation logic
- `showStatus()`: User feedback
- `calculateColumnCount()`: Dynamic column calculation

### Word API Integration

#### Table Creation Flow
1. Get Juz data from selection
2. Calculate column count based on options
3. Insert title and description paragraphs
4. Create table with appropriate dimensions
5. Style header row
6. Fill data rows
7. Set column widths
8. Apply styling

#### Key Word API Concepts
```javascript
// Running Word context
await Word.run(async (context) => {
  const body = context.document.body;
  
  // Insert paragraph
  const para = body.insertParagraph(text, Word.InsertLocation.end);
  
  // Create table
  const table = body.insertTable(rows, cols, Word.InsertLocation.end);
  
  // Sync changes
  await context.sync();
});
```

## Webpack Configuration

### Entry Points
- `polyfill`: Core-js and regenerator-runtime for IE11 support
- `taskpane`: Main taskpane functionality
- `commands`: Ribbon command handlers

### Loaders
- `babel-loader`: ES6+ transpilation
- `html-loader`: HTML processing
- `css-loader` + `style-loader`: CSS handling
- `file-loader`: Asset management

### Plugins
- `HtmlWebpackPlugin`: Generate HTML files
- `CopyWebpackPlugin`: Copy static assets

### Dev Server Configuration
- HTTPS enabled (required by Office)
- Port 3000
- Hot reload enabled
- CORS headers for cross-origin requests

## Manifest File

### Key Sections

#### Metadata
```xml
<Id>12345678-1234-1234-1234-123456789012</Id>
<Version>1.0.0.0</Version>
<ProviderName>Word Quran</ProviderName>
```

#### Permissions
```xml
<Permissions>ReadWriteDocument</Permissions>
```

#### Source Location
```xml
<SourceLocation DefaultValue="https://localhost:3000/taskpane.html"/>
```

#### Version Overrides
Defines:
- Ribbon UI customization
- Command buttons
- Icons and resources
- Taskpane behavior

## Testing

### Manual Testing
1. Start dev server: `npm run dev-server`
2. Sideload manifest in Word
3. Test each feature:
   - Juz selection
   - Option toggles
   - Table creation
   - Multiple tables
   - Error handling

### Sideloading the Add-in

#### On Windows
1. Save manifest.xml locally
2. Open Word
3. File → Options → Trust Center → Trust Center Settings
4. Trusted Add-in Catalogs
5. Add catalog URL
6. Insert → My Add-ins → Shared Folder

#### On Mac
1. Copy manifest to: `~/Library/Containers/com.microsoft.Word/Data/Documents/wef`
2. Restart Word
3. Insert → Add-ins → My Add-ins

#### On Word Online
1. Upload manifest via Admin Center
2. Or use Office Add-ins Store

### Testing Checklist
- [ ] All 30 Juz load correctly
- [ ] Options toggle work
- [ ] Tables create successfully
- [ ] Multiple tables in one document
- [ ] Error messages display correctly
- [ ] Mobile/responsive layout works
- [ ] Icons display properly
- [ ] Status messages appear/disappear

## Common Development Tasks

### Adding a New Juz Data Field
1. Update `quran-data.js` structure
2. Modify `getJuz()` if needed
3. Update UI to display new field
4. Update table creation logic
5. Test thoroughly

### Modifying Table Layout
1. Edit `createTable()` function
2. Adjust `calculateColumnCount()` if adding columns
3. Update column width allocation
4. Test with all option combinations

### Changing Styles
1. Edit inline CSS in `taskpane.html`
2. Or modify Word table styling in `taskpane.js`
3. Test in both light and dark modes (if applicable)

### Adding New Features
1. Update UI in `taskpane.html`
2. Add event handlers in `taskpane.js`
3. Implement Word API logic
4. Update documentation
5. Test edge cases

## Debugging

### Browser Console
Access via F12 in Word Desktop or browser console in Word Online

### Common Issues

#### Add-in Not Loading
- Check dev server is running
- Verify HTTPS certificate
- Check manifest URL matches server
- Clear Office cache

#### API Errors
- Check Office.js is loaded
- Verify API method names
- Ensure proper context.sync() calls
- Check for missing await statements

#### Styling Issues
- Test in multiple Word versions
- Check CSS specificity
- Verify HTML structure
- Test with different DPI settings

### Debug Logging
```javascript
console.log('Debug info:', variable);
console.error('Error:', error);
```

## Performance Considerations

### Optimization Tips
1. Minimize table size (default 20 rows)
2. Batch Word API operations
3. Use single context.sync() when possible
4. Lazy load data if needed
5. Minimize bundle size

### Bundle Analysis
```bash
# Add webpack-bundle-analyzer
npm install --save-dev webpack-bundle-analyzer

# Update webpack config to include analyzer
# Run build to see bundle composition
```

## Security

### Best Practices
- No eval() or Function() constructors
- Sanitize any user input
- Use HTTPS for all resources
- Keep dependencies updated
- Follow Office security guidelines

### Content Security Policy
The add-in should comply with Office's CSP requirements:
- No inline scripts (use webpack bundling)
- No external script loading at runtime
- HTTPS for all resources

## Contributing

### Code Style
- Use ESLint for linting
- Follow existing code patterns
- Add comments for complex logic
- Keep functions small and focused

### Git Workflow
1. Fork the repository
2. Create a feature branch
3. Make changes with clear commits
4. Test thoroughly
5. Submit pull request

### Commit Messages
Format: `type: description`

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation
- `style`: Formatting
- `refactor`: Code restructuring
- `test`: Adding tests
- `chore`: Maintenance

Example:
```
feat: add notes column option
fix: correct Juz 15 verse range
docs: update installation instructions
```

## Release Process

### Version Numbering
Follow semantic versioning: MAJOR.MINOR.PATCH

### Release Checklist
1. Update version in package.json
2. Update version in manifest.xml
3. Build production version
4. Test all features
5. Update CHANGELOG.md
6. Create git tag
7. Push to repository
8. Create GitHub release

## Resources

### Office Add-ins Documentation
- [Office Add-ins Overview](https://docs.microsoft.com/en-us/office/dev/add-ins/)
- [Word JavaScript API](https://docs.microsoft.com/en-us/office/dev/add-ins/reference/overview/word-add-ins-reference-overview)
- [Manifest Reference](https://docs.microsoft.com/en-us/office/dev/add-ins/develop/add-in-manifests)

### Tools
- [Office Add-in Validator](https://docs.microsoft.com/en-us/office/dev/add-ins/testing/troubleshoot-manifest)
- [Yo Office Generator](https://github.com/OfficeDev/generator-office)
- [Office Add-in Debugging](https://docs.microsoft.com/en-us/office/dev/add-ins/testing/debug-add-ins-overview)

### Community
- [Office Dev Center](https://developer.microsoft.com/en-us/office)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/office-js)
- [GitHub Issues](https://github.com/fareedsuri/wordquran/issues)

## Future Enhancements

### Potential Features
- [ ] Custom verse range input
- [ ] Multiple memorization methods
- [ ] Progress tracking across documents
- [ ] Export to other formats
- [ ] Integration with Quran APIs
- [ ] Audio links for recitation
- [ ] Tafsir (commentary) notes
- [ ] Review schedule generator
- [ ] Multiple language support
- [ ] Print-optimized layouts

### Technical Improvements
- [ ] Add unit tests
- [ ] Add integration tests
- [ ] Implement TypeScript
- [ ] Add ESLint configuration
- [ ] Set up CI/CD pipeline
- [ ] Add automated releases
- [ ] Improve error handling
- [ ] Add telemetry (privacy-respecting)
- [ ] Optimize bundle size
- [ ] Add offline support

## License
MIT License - See LICENSE file for details

## Contact
For technical questions or contributions:
- GitHub Issues: [fareedsuri/wordquran](https://github.com/fareedsuri/wordquran)
- Email: (See GitHub profile)
