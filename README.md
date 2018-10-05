1. Current Version: --
1. ReadMe Updated: --

# ACH File Library

Base library used to read and write fixed-length ACH files.

## Including in a Proejct

TBD

## Usage

### Creating a new ACH file.

```
var file = new AchFile(
  destination,
  origin,
  reference);
```

Batches are added to files:

```
var batch = file.AddBatch(
  description,
  effectiveEntryDate,
  discretionaryData
  );
```

Entry details are added to batches:

```
var detail = batch.AddEntryDetail(
  transactionCode,
  routingNumber,
  accountNumber,
  amount,
  name
  );
```

Files are written to a stream:

```
var file = new FileStream("c:/my file", FileMode.Open);
file.Write(stream);

var memory = new MemoryStream();
file.Write(stream);
```

### Reading an ACH file.

_Note: file reading is not yet supported._

```
var file = AchFile.Read("c:/my file");
```

Batches can be accessed as a read-only list:

`var batch = file.Batches.First();`

Individual records can be accessed from batches:

`var record = batch.Records.First();`

New files, or read files have some common information:

```
var debits = file.TotalDebitDollarAmount;
var credits = file.TotalCreditDollarAmount;
```

## Local Development

Requires.

1. PowerShell

Building the app.

1. Clone the repository.
1. `cd "<wherever you cloned it>"`
1. `.\Build.ps1`

As with most apps, this can be run by the VS debugger.

## Road Map

- v0.9
  - Development version.
- v1.0
  - Write ACH Files.
- v1.1
  - Read ACH Files.

## Version History

