 import { useAuthContext } from '../AuthContext';
import React, { useEffect, useRef, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

const Home = () => {
  
    const {user} = useAuthContext();
    const [allTasks, setAllTasks] = useState([]);
    const [currentTask, setCurrentTask] = useState('');

    
    const connectionRef = useRef(null);

    useEffect(() => {
        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/currentStatus").build();
            await connection.start();
           
            connectionRef.current = connection;

            connectionRef.current.invoke('getTasks');  
            connection.on('getTasks', tasks => {
                setAllTasks(tasks);
            });
            connection.on('addTask', tasks => {
                setAllTasks(tasks);
            });
            connection.on('markAsDoingTask', tasks => {
                setAllTasks(tasks);
            });
            connection.on('markAsDoneTask', tasks => {
                setAllTasks(tasks);
            });

          
        }


        connectToHub();
      
       
    }, []);
    const onAddClick =async()=>{
        connectionRef.current.invoke('addTask',{title: currentTask});  
        setCurrentTask('');
    }
    const onDoingClick =async(task)=>{
     
        connectionRef.current.invoke('markAsDoingTask',task);  
    }
    const onDoneClick =async(task)=>{
        connectionRef.current.invoke('markAsDoneTask',task);  
    }
 
    return <div id="root">
        <br/>
        <br/>
        <div>
      <div className="container">
          <div>
          <div className="row">
            <div className="col-md-10">
                <input type="text" className="form-control" placeholder="Task Title" value={currentTask} onChange={e=>(setCurrentTask(e.target.value))}/>
            </div>
            <div className="col-md-2">
                <button className="btn btn-primary btn-block" onClick={onAddClick}>Add Task</button>
            </div>
        </div>
        <br/>
        <br/>
              <table className="table table-hover table-striped table-bordered">
                  <thead>
                    <tr>
                        <th>Title</th>
                        <th>Status</th>
                    </tr>
                    </thead>
                    <tbody>
                    {allTasks && allTasks.map(p => <tr key={p.id}>
                        <td>{p.title}</td>
                        <td>{p.status==0&&<button className='btn btn-info' onClick={()=>onDoingClick(p)}>I'm doing this!</button>}
                        {((p.status==1||p.status==2)&&user.id!=p.userId)&&<button className='btn btn-warning' disabled='true'>{p.user.firstName} {p.user.lastName} is doing this</button>}
                        {((p.status==1||p.status==2)&&user.id==p.userId)&&<button className='btn btn-success'onClick={()=>onDoneClick(p)}>I am done!</button>}
                    
                        </td>
                       
                    </tr>)}
                     
                     </tbody>
                </table>
            </div>
        </div>
    </div>
        </div>
}

export default Home;