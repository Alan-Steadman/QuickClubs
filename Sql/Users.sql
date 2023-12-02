-- Users with UserProfile and Membership Count

SELECT
    U.Id
  , U.FirstName
  , U.LastName
  , U.Email

  , UP.DateOfBirth
  , UP.Address_Town

  , MembershipCount = (
		SELECT COUNT(1)
		FROM [Member] M
		WHERE M.UserId = U.Id
	)
FROM
    [User] U
	LEFT JOIN UserProfile UP ON UP.UserId = U.Id
