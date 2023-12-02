-- Membership Options & Levels for each Club

-- Things that users can sign up to to become a member of a club

SELECT

    ClubId = C.Id
  , ClubName = C.FullName
  , C.IsAffiliate

  , MembershipOptionId = MO.Id
  , MO.[Name]

  , MembershipLevelId = ML.Id
  , ML.[Name]

FROM
	Club C
	LEFT JOIN MembershipOption MO ON MO.ClubId = C.Id
	LEFT JOIN MembershipLevel ML ON ML.MembershipOptionId = MO.Id
