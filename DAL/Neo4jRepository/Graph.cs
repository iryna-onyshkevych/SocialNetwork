using DAL.Enteties;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Neo4JRepository
{
    public class Graph
    {
        private readonly IGraphClient _graphClient;

        public Graph()
        {
            _graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "SocialNetwork");
            _graphClient.Connect();
        }

        public IEnumerable<Person> FriendsOfAFriend(Person person)
        {
          
            var query = _graphClient.Cypher
                .Match("(p:Person)-[:FOLLOW]->(friend)-[:FOLLOW]->(foaf)")
                .Where((Person p) => p.Userlog == person.Userlog)
                .AndWhere("NOT (p)-[:FOLLOW]-(foaf)")
                .Return(foaf => foaf.As<Person>());
            return query.Results;
        }

        public IEnumerable<Person> CommonFriends(Person person1, Person person2)
        {
           

            var query = _graphClient.Cypher
                .Match("(p:Person)-[:FOLLOW]->(friend)-[:FOLLOW]->(foaf:Person)")
                .Where((Person p) => p.Userlog == person1.Userlog)
                .AndWhere((Person foaf) => foaf.Userlog == person2.Userlog)
                .Return(friend => friend.As<Person>());

            return query.Results;
        }

        public IEnumerable<string> ConnectingPaths(Person person1, Person person2)
        {
            
            var query = _graphClient.Cypher
                .Match("path = shortestPath((p1:Person)-[:FOLLOW*..6]->(p2:Person))")
                .Where((Person p1) => p1.Userlog == person1.Userlog)
                .AndWhere((Person p2) => p2.Userlog == person2.Userlog)
                .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(path) | n.nickname]"));

            return query.Results.Single();
        }

        public void CreatePerson(Person person)
        {
            _graphClient.Cypher
                .Create("(np:Person {newPerson})")
                .WithParam("newPerson", person)
                .ExecuteWithoutResults();
        }
        public void CreatRelationShip(Person whoStartFollow, Person whomFollow)
        {
            _graphClient.Cypher
                .Match("(p1:Person {nickname: {p1NickName}})", "(p2:Person {nickname: {p2NickName}})")
                .WithParam("p1NickName", whoStartFollow.Userlog)
                .WithParam("p2NickName", whomFollow.Userlog)
                .Create("(p1)-[:FOLLOW]->(p2)")
                .ExecuteWithoutResults();
        }
        public void DeleteRelationShip(Person whoStopFollow, Person whomFollow)
        {
            _graphClient.Cypher
              .Match("(p1:Person {nickname: {p1NickName}})-[r:FOLLOW]->(p2:Person {nickname: {p2NickName}})")
              .WithParam("p1NickName", whoStopFollow.Userlog)
              .WithParam("p2NickName", whomFollow.Userlog)
              .Delete("r")
              .ExecuteWithoutResults();
        }

    }
}
