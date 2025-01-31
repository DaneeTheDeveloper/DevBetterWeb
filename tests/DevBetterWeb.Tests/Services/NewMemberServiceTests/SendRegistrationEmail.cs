﻿using System.Threading.Tasks;
using DevBetterWeb.Core.Entities;
using DevBetterWeb.Core.Interfaces;
using DevBetterWeb.Core.Services;
using Xunit;
using Moq;

namespace DevBetterWeb.Tests.Services.NewMemberServiceTests
{
  public class SendRegistrationEmail
  {
    private readonly Mock<IRepository<Member>> _memberRepository;
    private readonly Mock<IRepository<Invitation>> _invitationRepository;
    private readonly Mock<IUserRoleMembershipService> _userRoleMembershipService;
    private readonly Mock<IPaymentHandlerSubscription> _paymentHandlerSubscription;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<IMemberRegistrationService> _memberRegistrationService;

    private readonly INewMemberService _newMemberService;

    private readonly string _email = "TestEmail";
    private readonly string _subscriptionId = "TestSubscriptionId";
    private readonly string _inviteCode = "TestInviteCode";

    private readonly Invitation _invitation;

    public SendRegistrationEmail()
    {
      _memberRepository = new Mock<IRepository<Member>>();
      _invitationRepository = new Mock<IRepository<Invitation>>();
      _userRoleMembershipService = new Mock<IUserRoleMembershipService>();
      _paymentHandlerSubscription = new Mock<IPaymentHandlerSubscription>();
      _emailService = new Mock<IEmailService>();
      _memberRegistrationService = new Mock<IMemberRegistrationService>();
      _newMemberService = new NewMemberService(_memberRepository.Object,
        _invitationRepository.Object,
        _userRoleMembershipService.Object,
        _paymentHandlerSubscription.Object,
        _emailService.Object,
        _memberRegistrationService.Object);
      _invitation = new Invitation(_email, _inviteCode, _subscriptionId);
    }

    [Fact]
    public async Task SendsRegistrationEmail()
    {
      await _newMemberService.SendRegistrationEmailAsync(_invitation);

      _emailService.Verify(e => e.SendEmailAsync(_email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

  }

}
