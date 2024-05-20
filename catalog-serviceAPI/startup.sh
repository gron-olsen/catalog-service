export server="localhost"
export port="27017"
export connectionString="mongodb://localhost:27017/"
export database="catalogDB"
export collection="catalogCol"
export AuctionConnection="http://localhost:5230"
echo $database $collection $connectionString
dotnet run server="$server" port="$port"
#chmod +x ./startup.sh
#./startup.sh