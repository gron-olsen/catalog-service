export server="localhost"
export port="27017"
export connectionString="mongodb://localhost:27017/"
export database="catalogDB"
export collection="catalogCol"
export connAuk="http://localhost:5289"
echo $database $connectionString
dotnet run server="$server" port="$port"