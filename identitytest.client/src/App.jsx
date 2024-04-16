import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [users, setUsers] = useState();


    useEffect(() => {
        getUsers();
    }, []);

    const contents = users === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <>
        
        <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {users.map(user =>
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.email}</td>
                    </tr>
                )}
            </tbody>
            </table>
        </> 
        ;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
    
    async function getUsers() {
        const response = await fetch('user/getappusers')
        console.log(response)
        const data = await response.json();

        setUsers(data);
    }
}

export default App;