<%@ page contentType="text/html;charset=UTF-8" isELIgnored="false" %>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<head>
    <title>Add data</title>
</head>
<body>
<a href="${pageContext.request.contextPath}/">Go back</a>
<div align="center">
    <form method="post">
        <label>
            <input name="workerID" type="text" value="0" style="display: none" />
        </label>
        <label>
            Surname
            <input align="center" name="surname" size="15" type="text" value="${worker.surname}"/>
            <br>
        </label>
        <label>
            Name
            <input align="center" name="name" size="15" type="text" value="${worker.name}"/>
            <br>
        </label>
        <label>
            Qualification ID
            <input align="center" name="qualID" size="15" type="text" value="${worker.qualID}"/>
            <br>
        </label>
        <button type="submit">submit</button>
    </form>
</div>
</body>
</html>
