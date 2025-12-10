# Using the _PDMS_Saleem_QuranFont_Circled_1-300_F000.ttf Font

## Installation Steps

### 1. Install the Font on Your System

**Windows:**
1. Locate the `_PDMS_Saleem_QuranFont_Circled_1-300_F000.ttf` file
2. Right-click on the font file
3. Select "Install" or "Install for all users"
4. The font will be installed (name detected automatically by the add-in)

**macOS:**
1. Double-click the `_PDMS_Saleem_QuranFont_Circled_1-300_F000.ttf` file
2. Click "Install Font" in Font Book
3. The font will be available

### 2. Font Implementation in Code

The code has been updated to use the PDMS Saleem QuranFont:

- **Font File:** `_PDMS_Saleem_QuranFont_Circled_1-300_F000.ttf`
- **Unicode Mapping:** Circled numbers 1-300 start at **U+F000**:
  - Number 1 ? U+F000
  - Number 2 ? U+F001
  - Number 3 ? U+F002
  - Number 300 ? U+F12B

### 3. How the Code Works

The `GetCircledNumber(int number)` method:
- Converts any number (1-300) to Unicode starting at U+F000
- Formula: `U+F000 + (number - 1)`
- Example: For verse 1, it returns `char.ConvertFromUtf32(0xF000)` = 
- Example: For verse 100, it returns `char.ConvertFromUtf32(0xF063)` = ??

The `DetectCircledFontName()` method:
- Auto-detects the font name as Windows registered it
- Tries variations like:
  - "PDMS_Saleem_QuranFont_Circled_1-300_F000"
  - "PDMS Saleem QuranFont Circled 1-300 F000"
  - "_PDMS_Saleem_QuranFont_Circled_1-300_F000"

### 4. Testing the Font

After installing the font:

1. **Restart Word completely** (close all instances)
2. Open Word and run the add-in
3. Create a table with "Make Table" button
4. Run the code with "Run Code" button
5. **A message box will appear** showing the detected font name
6. The verse numbers should appear as beautiful circled numbers

### 5. Manual Testing in Word

To verify the font is working:
1. In Word, go to **Insert ? Symbol ? More Symbols**
2. Select font: **PDMS_Saleem_QuranFont_Circled_1-300_F000** (or similar)
3. Change "from:" to **Unicode (hex)**
4. Type `F000` in the "Character code:" field
5. You should see the circled number ?
6. Type `F063` to see ?? (number 100)

### 6. Troubleshooting

**If circled numbers don't appear:**
- Verify the font is installed: Settings ? Personalization ? Fonts (Windows)
- Restart Word after installing the font
- The add-in will show which font name it detected

**If you see squares or question marks:**
- The font may not be installed correctly
- Try reinstalling the font as Administrator
- Make sure the TTF file is not corrupted

## Font Specification

- **Font File:** `_PDMS_Saleem_QuranFont_Circled_1-300_F000.ttf`
- **Range:** Circled numbers 1 to 300
- **Format:** TrueType Font (.ttf)
- **Unicode Block:** Private Use Area (PUA) starting at **U+F000**
- **Start Code Point:** U+F000 (number 1)
- **End Code Point:** U+F12B (number 300)
- **Total Glyphs:** 300 circled numbers
- **Purpose:** Special Quran font with Arabic text support and circled verse numbers

## Code Implementation

### Key Changes:

1. **Font constant updated:**
   ```csharp
   private const string CIRCLED_FONT_NAME = "PDMS_Saleem_QuranFont_Circled_1-300_F000";
   ```

2. **Unicode mapping corrected:**
   ```csharp
   int unicodePoint = 0xF000 + (number - 1);
   ```
   - Verse 1 ? U+F000
   - Verse 2 ? U+F001
   - Verse 286 ? U+F11D

3. **Auto-detection of font name variations**
4. **Font applied only to circled numbers**, Arabic text keeps its original font

## Result

Each verse will display its number using the PDMS Saleem QuranFont circled numbers at the beginning of the first (leftmost) column:
- ? for verse 1
- ? for verse 10
- ? for verse 20
- ?? for verse 290 (if the font supports multi-char display)

The font is specifically designed for Quran display with proper Arabic rendering and integrated circled verse numbers.
