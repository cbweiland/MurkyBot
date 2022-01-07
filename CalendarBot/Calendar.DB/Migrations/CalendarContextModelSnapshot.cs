﻿// <auto-generated />
using System;
using Calendar.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Calendar.DB.Migrations
{
    [DbContext(typeof(CalendarContext))]
    partial class CalendarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Calendar.DB.Models.ChannelConfig", b =>
                {
                    b.Property<int>("ChannelConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ChannelId")
                        .HasColumnType("text");

                    b.Property<int>("ChannelType")
                        .HasColumnType("int");

                    b.Property<string>("GuildId")
                        .HasColumnType("text");

                    b.HasKey("ChannelConfigId");

                    b.ToTable("ChannelConfigs");
                });

            modelBuilder.Entity("Calendar.DB.Models.EventMeeting", b =>
                {
                    b.Property<int>("EventMeetingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CheckIn")
                        .HasColumnType("text");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DiscordMessageId")
                        .HasColumnType("text");

                    b.Property<string>("EventType")
                        .HasColumnType("text");

                    b.Property<string>("Time")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("EventMeetingId");

                    b.ToTable("EventMeetings");
                });

            modelBuilder.Entity("Calendar.DB.Models.SignUp", b =>
                {
                    b.Property<int>("SignUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeSignedUp")
                        .HasColumnType("datetime");

                    b.Property<string>("EmoteClicked")
                        .HasColumnType("text");

                    b.Property<int>("EventMeetingId")
                        .HasColumnType("int");

                    b.Property<string>("PersonDiscordId")
                        .HasColumnType("text");

                    b.HasKey("SignUpId");

                    b.HasIndex("EventMeetingId");

                    b.ToTable("SignUp");
                });

            modelBuilder.Entity("Calendar.DB.Models.SignUp", b =>
                {
                    b.HasOne("Calendar.DB.Models.EventMeeting", "EventMeeting")
                        .WithMany("SignUps")
                        .HasForeignKey("EventMeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventMeeting");
                });

            modelBuilder.Entity("Calendar.DB.Models.EventMeeting", b =>
                {
                    b.Navigation("SignUps");
                });
#pragma warning restore 612, 618
        }
    }
}
