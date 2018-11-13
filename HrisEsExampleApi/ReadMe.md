# HRIS Event Store

This solution demonstrates basic interactions with Event Store. It shows one way to build an aggregate. Interactions with that aggregate are made possible via a .NET Core API.

# Supported Endpoints

## Viewing Employees

`[GET] /api/employees` - Returns a list of employees.

```json
{
    {
        "name" : "mark",
        "salary" : 100000,
        "employed" : true
    },
    {
        "name" : "joe",
        "salary" : 0,
        "employed" : false
    }
}
```

`[POST] /api/employees/{guid:id}` - Creates an employee.


``` json
{
    "name" : "marky mark"
    "salary" : "100000"
}
```

## Salaries

`[POST] /api/employees/{guid:id}/salary`

Sets the employees salary.

``` json
{
    "salary" : "40000"
    "reason" : "because, mark works really hard."
}
```

- Returns an error if the employee is termianted.


## Termination

`[POST] /api/employees/{guid:id}/terminate`

``` json
{
    "reason" : "mark was a jerk"
}
```

`[GET] /api/employees/{guid:id}` - Returns an individual employee.


