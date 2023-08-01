select * from project where  exists  (select project_id from employ_to_proj  where empl_id =1005 );





select task.task_id,task.task_name,task.task_description,task.task_startdate,task.task_enddate,task.task_statue,project.project_name
                    from task  inner join project  on task.project_id = project.project_id  
                    where exists (select task_id from employ_to_task  where employ_id = 1)