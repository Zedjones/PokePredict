PokePredict
==========
Given a set of Pokemon along with their movesets, give the optimal team to defeat them (with a to be defined cost function).

Since this cost function might take a while to run, each user will have a queue of jobs and will be able to check the results afterwards on the web view afterwards.

## Requirements
If you're setting up the Veekun database, you'll need Python 2 and potentially virtualenv if you want to keep your global packages clean.

Otherwise, you'll just need .NET Core.

## Set up and running
### Setting up the database
In the future, I'll write a script to do this automatically. For now, you can do these steps to get things running manually: 
 - Clone the Veekun submodule: `git submodule update --init`
 - Change to the directory: `cd pokedex`
 - Create a new virtualenv: `virtualenv --python=python2 ./`
 - Run the setup: `bin/python2 setup.py develop`
 - Load the data: `bin/pokedex load`
 - Build the SQLite database `bin/pokedex reindex`

The data is now contained in: `pokedex/data/pokedex.sqlite`

### Running the server
Assuming you're using .NET Core, restore dependencies and run:
```
dotnet restore
dotnet build --configuration Release
bin/Release/netcoreapp3.1/PokePredict
```