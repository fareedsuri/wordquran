/* global document, Office, Word */

import { getAllJuzNames, getJuz } from '../data/quran-data.js';

Office.onReady((info) => {
  if (info.host === Office.HostType.Word) {
    document.getElementById('create-table-btn').onclick = createTable;
    document.getElementById('juz-select').onchange = handleJuzSelection;
    
    // Populate the juz dropdown
    populateJuzDropdown();
  }
});

function populateJuzDropdown() {
  const select = document.getElementById('juz-select');
  const juzNames = getAllJuzNames();
  
  juzNames.forEach(juz => {
    const option = document.createElement('option');
    option.value = juz.value;
    option.textContent = juz.label;
    select.appendChild(option);
  });
}

function handleJuzSelection() {
  const select = document.getElementById('juz-select');
  const createBtn = document.getElementById('create-table-btn');
  
  if (select.value) {
    createBtn.disabled = false;
  } else {
    createBtn.disabled = true;
  }
}

function showStatus(message, type = 'info') {
  const statusDiv = document.getElementById('status');
  statusDiv.textContent = message;
  statusDiv.className = `status ${type}`;
  statusDiv.style.display = 'block';
  
  // Hide after 5 seconds for success messages
  if (type === 'success') {
    setTimeout(() => {
      statusDiv.style.display = 'none';
    }, 5000);
  }
}

async function createTable() {
  const select = document.getElementById('juz-select');
  const juzNumber = parseInt(select.value);
  
  if (!juzNumber) {
    showStatus('Please select a Juz first', 'error');
    return;
  }

  const includePageNumbers = document.getElementById('include-page-numbers').checked;
  const includeDateColumn = document.getElementById('include-date-column').checked;
  const includeNotesColumn = document.getElementById('include-notes-column').checked;
  
  const juzInfo = getJuz(juzNumber);
  
  if (!juzInfo) {
    showStatus('Error: Juz information not found', 'error');
    return;
  }

  showStatus('Creating table...', 'info');
  
  try {
    await Word.run(async (context) => {
      // Insert title
      const body = context.document.body;
      const titleParagraph = body.insertParagraph(`Memorization Table - ${juzInfo.name}`, Word.InsertLocation.end);
      titleParagraph.style = 'Heading 1';
      titleParagraph.alignment = Word.Alignment.centered;
      
      // Insert juz information
      const infoParagraph = body.insertParagraph(
        `${juzInfo.startSurahName} (${juzInfo.startVerse}) to ${juzInfo.endSurahName} (${juzInfo.endVerse})`,
        Word.InsertLocation.end
      );
      infoParagraph.alignment = Word.Alignment.centered;
      infoParagraph.font.italic = true;
      infoParagraph.spaceAfter = 20;
      
      // Create table
      const columnCount = calculateColumnCount(includePageNumbers, includeDateColumn, includeNotesColumn);
      const rowCount = 21; // 1 header row + 20 data rows
      
      const table = body.insertTable(rowCount, columnCount, Word.InsertLocation.end);
      table.styleBuiltIn = Word.BuiltInStyleName.gridTable1Light;
      table.headerRowCount = 1;
      
      // Set up headers
      let colIndex = 0;
      table.getCell(0, colIndex++).value = 'Day';
      
      if (includePageNumbers) {
        table.getCell(0, colIndex++).value = 'Pages';
      }
      
      table.getCell(0, colIndex++).value = 'Verses';
      
      if (includeDateColumn) {
        table.getCell(0, colIndex++).value = 'Date';
      }
      
      table.getCell(0, colIndex++).value = 'Completed';
      
      if (includeNotesColumn) {
        table.getCell(0, colIndex++).value = 'Notes';
      }
      
      // Fill in the data rows
      for (let row = 1; row < rowCount; row++) {
        colIndex = 0;
        
        // Day number
        table.getCell(row, colIndex++).value = `Day ${row}`;
        
        // Pages (if enabled)
        if (includePageNumbers) {
          // Estimate page range (Quran is typically ~600 pages, 30 juz)
          const startPage = Math.floor((juzNumber - 1) * 20) + Math.floor((row - 1) * 1);
          const endPage = startPage + 1;
          table.getCell(row, colIndex++).value = `${startPage}-${endPage}`;
        }
        
        // Verses placeholder
        table.getCell(row, colIndex++).value = `Section ${row}`;
        
        // Date (if enabled)
        if (includeDateColumn) {
          table.getCell(row, colIndex++).value = '';
        }
        
        // Completed checkbox placeholder
        table.getCell(row, colIndex++).value = '☐';
        
        // Notes (if enabled)
        if (includeNotesColumn) {
          table.getCell(row, colIndex++).value = '';
        }
      }
      
      // Style the header row
      const headerRow = table.rows.getFirst();
      headerRow.font.bold = true;
      headerRow.font.color = '#FFFFFF';
      headerRow.shadingColor = '#2C3E50';
      
      // Set column widths
      colIndex = 0;
      table.columns.getItemAt(colIndex++).width = 60; // Day
      
      if (includePageNumbers) {
        table.columns.getItemAt(colIndex++).width = 60; // Pages
      }
      
      table.columns.getItemAt(colIndex++).width = 100; // Verses
      
      if (includeDateColumn) {
        table.columns.getItemAt(colIndex++).width = 80; // Date
      }
      
      table.columns.getItemAt(colIndex++).width = 70; // Completed
      
      if (includeNotesColumn) {
        table.columns.getItemAt(colIndex++).width = 150; // Notes
      }
      
      // Add a paragraph break after the table
      body.insertParagraph('', Word.InsertLocation.end);
      
      await context.sync();
      
      showStatus('Table created successfully!', 'success');
    });
  } catch (error) {
    console.error('Error creating table:', error);
    showStatus(`Error: ${error.message}`, 'error');
  }
}

function calculateColumnCount(includePageNumbers, includeDateColumn, includeNotesColumn) {
  let count = 3; // Day, Verses, Completed (always present)
  if (includePageNumbers) count++;
  if (includeDateColumn) count++;
  if (includeNotesColumn) count++;
  return count;
}
