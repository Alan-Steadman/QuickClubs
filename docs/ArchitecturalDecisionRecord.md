# Architectural Decision Record

## Contracts

### The Issue

The original plan was to have Api contracts as public sealed records, which are immutable.

Most of the the contracts are public sealed records.

Some contracts have been adapted for use in Blazor (in the prototype AdminUI) as models for , and have been converted to mutable classes, with mutable public properties that have data annotations, in order to proviode the functionality needed for a basic view model.  These contracts are:
- CreateClubRequest
- LoginRequest

### The Solution

The final plan is to go back to the original plan - have all Api contracts as immutable public sealed records with no data annotations, and to create addional mutable classes for use as models in blazor.  This may be yet another layer of DTOs, but they all have their specific purpose and are tailored to meet those rquirements.  Api contracts should be discreet of view models.

This solution should be implemented at the point that the final blazor admin ui is created and the prototype admin ui is binned.