<%@ page contentType="text/html;charset=UTF-8" isELIgnored="false" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<head>
    <title>Add data</title>
</head>
<body>
<a href="${pageContext.request.contextPath}/">Go back</a>
<div align="center">
    <form name="addForm" method="post">
        <label>
            Worker ID
            <input align="center" name="workerID" size="15" type="text"/>
            <br>
        </label>
        <label>
            Surname
            <input align="center" name="surname" size="15" type="text"/>
             <br>
        </label>
        <label>
            Name
            <input align="center" name="name" size="15" type="text"/>
            <br>
        </label>
        <label>
            Qualification ID
            <input align="center" name="qualID" size="15" type="text"/>
            <br>
        </label>
        <button type="submit">submit</button>
    </form>
</div>
</body>
</html>
