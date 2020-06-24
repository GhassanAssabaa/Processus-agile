using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assests.Scripts;

namespace Tests
{
    public class UsersManagerTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UsersManagerTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator UsersManagerTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator RegisterTest()
        {
            string userName = "userNameTest";
            string passworld = "passWordTest";
            List<User> initialUsers;
            List<User> newUsers;

            var usersManager = new GameObject();
            usersManager.gameObject.AddComponent<UsersManager>();
            initialUsers = usersManager.GetComponent<UsersManager>().GetUsers();
            Debug.Log(initialUsers.Count);
            usersManager.GetComponent<UsersManager>().WriteUserData(new User(userName, passworld));
            newUsers = usersManager.GetComponent<UsersManager>().GetUsers();
            Debug.Log(newUsers.Count);

            Assert.AreEqual(initialUsers.Count + 1, newUsers.Count);
            Assert.AreEqual(newUsers[newUsers.Count - 1].userName, userName);
            Assert.AreEqual(newUsers[newUsers.Count - 1].password, passworld);

            usersManager.GetComponent<UsersManager>().ResetUsersData(initialUsers);
            yield return null;
        }

    }
}
